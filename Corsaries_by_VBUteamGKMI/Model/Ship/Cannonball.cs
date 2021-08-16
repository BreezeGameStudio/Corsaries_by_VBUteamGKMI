using Corsaries_by_VBUteamGKMI.Model;
using Corsaries_by_VBUteamGKMI.Model.Ship;
using Corsaries_by_VBUteamGKMI.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Corsaries_by_VBUteamGKMI.Model
{
    public enum Cannonball_side { Left, Right }
    public class Cannonball
    {
        public Cannonball_side _side_type;
        public int _speed;
        public int _range;
        public Texture2D _current_sprite; // текущий спрайт для отрисовки
        public Vector2 _position; // позицыя
        public Direction _direction { get; set; } // направление движения
        public Cannonball(Cannon cannon, Cannonball_side type, Vector2 position, Direction direction)
        {
            _side_type = type;
            _direction = direction;
            _current_sprite = createCircleText(5);
            _position = position;
            _speed = cannon._speed;
            _range = cannon._range;
        }
        Texture2D createCircleText(int radius)
        {
            Texture2D texture = new Texture2D(Game1._graphics.GraphicsDevice, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = Color.Black;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }
        public void Move()
        {
            switch (_direction)
            {
                case Direction.up:
                    switch (_side_type)
                    {
                        case Cannonball_side.Left:
                            Go_L();
                            break;
                        case Cannonball_side.Right:
                            Go_R();
                            break;
                        default:
                            break;
                    }
                    break;
                case Direction.up_right:
                    switch (_side_type)
                    {
                        case Cannonball_side.Left:
                            Go_UL();
                            break;
                        case Cannonball_side.Right:
                            Go_DR();
                            break;
                        default:
                            break;
                    }
                    break;
                case Direction.right:
                    switch (_side_type)
                    {
                        case Cannonball_side.Left:
                            Go_U();
                            break;
                        case Cannonball_side.Right:
                            Go_D();
                            break;
                        default:
                            break;
                    }
                    break;
                case Direction.right_down:
                    switch (_side_type)
                    {
                        case Cannonball_side.Left:
                            Go_UR();
                            break;
                        case Cannonball_side.Right:
                            Go_DL();
                            break;
                        default:
                            break;
                    }
                    break;
                case Direction.down:
                    switch (_side_type)
                    {
                        case Cannonball_side.Left:
                            Go_R();
                            break;
                        case Cannonball_side.Right:
                            Go_L();
                            break;
                        default:
                            break;
                    }
                    break;
                case Direction.down_left:
                    switch (_side_type)
                    {
                        case Cannonball_side.Left:
                            Go_DR();
                            break;
                        case Cannonball_side.Right:
                            Go_UL();
                            break;
                        default:
                            break;
                    }
                    break;
                case Direction.left:
                    switch (_side_type)
                    {
                        case Cannonball_side.Left:
                            Go_D();
                            break;
                        case Cannonball_side.Right:
                            Go_U();
                            break;
                        default:
                            break;
                    }
                    break;
                case Direction.left_up:
                    switch (_side_type)
                    {
                        case Cannonball_side.Left:
                            Go_DL();
                            break;
                        case Cannonball_side.Right:
                            Go_UR();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        #region методы перемещения
        public virtual void Go_U() // вверх
        {
            if (_range > 0)
            {
                _position.Y -= _speed;
                _range--;
            }
           
        }
        public virtual void Go_UL() // вверх лево
        {
            if (_range > 0)
            {
                _position.Y -= _speed;
                _position.X -= _speed;
                _range--;
            }
           
        }
        public virtual void Go_UR() // вверх право
        {
            if (_range > 0)
            {
                _position.Y -= _speed;
                _position.X += _speed;
                _range--;
            }
           
        }
        public virtual void Go_D()  // вниз
        {
            if (_range > 0)
            {
                _position.Y += _speed;
                _range--;
            }
           
        }
        public virtual void Go_DL()  // вниз лево
        {
            if (_range > 0)
            {
                _position.Y += _speed;
                _position.X -= _speed;
                _range--;
            }
           
        }
        public virtual void Go_DR()  // вниз право
        {
            if (_range > 0)
            {
                _position.Y += _speed;
                _position.X += _speed;
                _range--;
            }
           
        }
        public virtual void Go_L() // в лево
        {
            if (_range > 0)
            {
                _position.X -= _speed;
                _range--;
            }
           
        }
        public virtual void Go_R() // в право
        {
            if (_range > 0)
            {
                _position.X += _speed;
                _range--;
            }
           
        }
        #endregion
    }
}
