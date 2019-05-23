﻿using MagicalLifeAPI.Components.Entity;
using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.DataTypes.Attribute;
using MagicalLifeAPI.Entity.Humanoid;
using MagicalLifeAPI.Entity.Util.Modifier;
using MagicalLifeAPI.Filing;
using MagicalLifeAPI.GUI;
using MagicalLifeAPI.Networking.Client;
using MagicalLifeAPI.Networking.Messages;
using MagicalLifeAPI.Networking.World.Modifiers;
using MagicalLifeAPI.Pathfinding;
using MagicalLifeAPI.Properties;
using MagicalLifeAPI.Sound;
using MagicalLifeAPI.Util;
using MagicalLifeAPI.World.Base;
using System;

namespace MagicalLifeAPI.Entity.Movement
{
    /// <summary>
    /// Used to move entities.
    /// </summary>
    public static class EntityWorldMovement
    {
        /// <summary>
        /// Moves the entity as far along on its path as possible.
        /// </summary>
        /// <param name="entity"></param>
        public static void MoveEntity(Living entity)
        {
            ComponentMovement movementComponent = entity.GetExactComponent<ComponentMovement>();
            ProtoQueue<PathLink> path = movementComponent.QueuedMovement;

            while (movementComponent.Movement.GetValue() > 0 && path.Count > 0)
            {
                PathLink section = path.Peek();

                Tile sourceTile = World.Data.World.Dimensions[entity.Dimension][section.Origin.X, section.Origin.Y];
                Tile destinationTile = World.Data.World.Dimensions[entity.Dimension][section.Destination.X, section.Destination.Y];
                Move(entity, sourceTile, destinationTile);
            }
        }

        /// <summary>
        /// Determines how many movement Point2Ds are deducted from the entity in motion for a given movement action.
        /// </summary>
        /// <param name="xMove"></param>
        /// <param name="yMove"></param>
        /// <returns></returns>
        private static double CalculateMovementReduction(double xMove, double yMove)
        {
            if (Math.Abs(xMove) == Math.Abs(yMove))
            {
                return xMove;
            }
            else
            {
                return xMove + yMove;
            }
        }

        /// <summary>
        /// Moves the entity in a direction.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void Move(Living entity, Tile source, Tile destination)
        {
            ComponentSelectable sLocation = source.GetExactComponent<ComponentSelectable>();
            ComponentSelectable dLocation = destination.GetExactComponent<ComponentSelectable>();
            Direction direction = DetermineMovementDirection(sLocation.MapLocation, dLocation.MapLocation);

            float xMove = 0;
            float yMove = 0;

            AnimatedTexture animated = (AnimatedTexture)entity.Visual;

            switch (direction)
            {
                case Direction.North:
                    yMove = -1;
                    animated.StartSequence(Human.UpSequence);
                    break;

                case Direction.South:
                    yMove = 1;
                    animated.StartSequence(Human.DownSequence);
                    break;

                case Direction.East:
                    xMove = 1;
                    animated.StartSequence(Human.RightSequence);
                    break;

                case Direction.West:
                    xMove = -1;
                    animated.StartSequence(Human.LeftSequence);
                    break;

                case Direction.NorthWest:
                    xMove = -1;
                    yMove = -1;
                    break;

                case Direction.NorthEast:
                    xMove = 1;
                    yMove = -1;
                    break;

                case Direction.SouthWest:
                    xMove = -1;
                    yMove = 1;
                    break;

                case Direction.SouthEast:
                    xMove = 1;
                    yMove = 1;
                    break;

                default:
                    throw new InvalidOperationException("Unexpected value for direction: " + direction.ToString());
            }

            ComponentMovement movementComponent = entity.GetExactComponent<ComponentMovement>();
            xMove *= (float)movementComponent.Movement.GetValue();
            yMove *= (float)movementComponent.Movement.GetValue();

            float movementPenalty = (float)Math.Abs(CalculateMovementReduction(xMove, yMove)) * -1;

            if (MathUtil.GetDistance(movementComponent.TileLocation, dLocation.MapLocation) > movementComponent.Movement.GetValue())
            {
                //The character fell short of reaching the next tile
                movementComponent.TileLocation = new Point2DDouble((float)movementComponent.TileLocation.X + xMove, (float)movementComponent.TileLocation.Y + yMove);
                FootStepSound(entity, source);
            }
            else
            {
                //The character made it to the next tile.
                entity.GetExactComponent<ComponentSelectable>().MapLocation = dLocation.MapLocation;
                movementComponent.TileLocation = new DataTypes.Point2DDouble(dLocation.MapLocation.X, dLocation.MapLocation.Y);
                movementComponent.QueuedMovement.Dequeue();
                movementPenalty = (float)MathUtil.GetDistance(movementComponent.TileLocation, dLocation.MapLocation);
                FootStepSound(entity, destination);

                //If this entity is the current client's and therefore that clients responsibility to report about
                if (entity.PlayerID == SettingsManager.PlayerSettings.Settings.PlayerID)
                {
                    ClientSendRecieve.Send(new WorldModifierMessage(new LivingLocationModifier(entity.ID, sLocation.MapLocation, dLocation.MapLocation, entity.Dimension)));
                }
            }

            movementComponent.Movement.AddModifier(new ModifierDouble(movementPenalty, new TimeRemoveCondition(1), Lang.NormalMovement));
        }

        private static void FootStepSound(Living living, Tile footStepsOn)
        {
            ComponentMovement movementComponent = living.GetExactComponent<ComponentMovement>();
            if (movementComponent.FootStepTimer.Allow())
            {
                Point2DFloat screenLocation = new Point2DFloat((float)movementComponent.TileLocation.X, (float)movementComponent.TileLocation.Y);

                screenLocation.X *= Tile.GetTileSize().X;
                screenLocation.Y *= Tile.GetTileSize().Y;

                FMODUtil.RaiseEvent(SoundsTable.FootSteps, "Material", footStepsOn.FootStepSound, screenLocation.ToPoint2D());
            }
        }

        /// <summary>
        /// Determines which way a creature is moving in terms of cardinal direction.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static Direction DetermineMovementDirection(Point2D source, Point2D destination)
        {
            if (destination.Y < source.Y)
            {
                if (destination.X > source.X)
                {
                    return Direction.NorthEast;
                }

                if (destination.X < source.X)
                {
                    return Direction.NorthWest;
                }

                return Direction.North;
            }

            if (destination.Y > source.Y)
            {
                if (destination.X > source.X)
                {
                    return Direction.SouthEast;
                }

                if (destination.X < source.X)
                {
                    return Direction.SouthWest;
                }

                return Direction.South;
            }

            if (destination.Y == source.Y)
            {
                if (destination.X > source.X)
                {
                    return Direction.East;
                }

                if (destination.X < source.X)
                {
                    return Direction.West;
                }
            }

            throw new InvalidOperationException("Unexpected movement direction found.");
        }
    }
}