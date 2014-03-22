using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpriteSpace;

namespace RandomWorld
{
    class Level : Structure
    {
        public const int MAX_TILES_X = 16;
        public const int MAX_TILES_Y = 12;
        public const int SPRITE_SIZE = 40;

        public struct Room
        {
            public int[,] _room;
            public int Size_X;
            public int Size_Y;

            //public int X_Mid;
            //public int Y_Mid;

            public int start_X;
            public int start_Y;
        }
      
        Room newRoom;
        Random rand = new Random();
        Sprite[,] GameBoard = new Sprite[MAX_TILES_X, MAX_TILES_Y];

        int numofRooms;
        int finishedRooms = 0;

        List<Room> roomList = new List<Room>();
        Sprite tiles;

        bool isValid;

        public Level()
        {
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            for (int b = 0; b < MAX_TILES_X; b++)
            {
                for (int c = 0; c < MAX_TILES_Y; c++)
                {

                    if (GameBoard[b, c] != null)
                    {
                        GameBoard[b, c].DrawMethod(spriteBatch, gameTime);
                    }
                }
                
            }
        }
        public override void Load(ContentManager content)
        {

            numofRooms = rand.Next(5, 8);
            finishedRooms = 0;
            while (finishedRooms < numofRooms)
            {
                newRoom = new Room();
                newRoom._room = new int[MAX_TILES_X, MAX_TILES_Y];
                ClearArray(newRoom._room);
                newRoom.start_X = rand.Next(0, MAX_TILES_X);
                newRoom.start_Y = rand.Next(0, MAX_TILES_Y);
                newRoom.Size_X = rand.Next(3, 6);
                newRoom.Size_Y = rand.Next(3, 6);
                isValid = CheckLocation(newRoom.Size_X, newRoom.Size_Y, newRoom.start_X, newRoom.start_Y, newRoom._room, GameBoard);
                if (isValid)
                {
                    finishedRooms++;
                    //Console.WriteLine("This is how many ROOMS");
                    roomList.Add(newRoom);
                    SetGameBoard(newRoom, GameBoard, content);
                }
                else
                {
                    //Console.WriteLine("Invalid ROOM");
                }
 
            }

            
        }
        public override void Update(GameTime gameTime)
        {

        }
 

        public override bool CheckLocation(int size_x, int size_y, int start_pos_X, int start_pos_Y, int[,] array, Sprite[,] gameboard)
        {
            if (start_pos_X > 0 && start_pos_X + size_x < MAX_TILES_X && start_pos_Y > 0 && start_pos_Y + size_y < MAX_TILES_Y)
            {
                for (int x = start_pos_X; x <= start_pos_X + size_x; x++)
                {
                    for (int y = start_pos_Y; y <= start_pos_Y + size_y; y++)
                    {
                        if (gameboard[x, y] != null)
                        {
                            return false;
                        }
                        else if (array[x, y] != 0)
                        {
                            return false;
                        }
                        else
                        {
                            array[x, y] = 1;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ClearArray(int[,] array)
        {
            for (int i = 0; i < MAX_TILES_X; i++)
            {
                for (int j = 0; j < MAX_TILES_Y; j++)
                {
                    array[i, j] = 0;
                }
            }
        }
        public void SetGameBoard(Room room, Sprite[,] gameBoard, ContentManager content)
        {
            for (int x = room.start_X; x < (room.Size_X + room.start_X); x++)
            {
                for (int y = room.start_Y; y < (room.Size_Y + room.start_Y); y++)
                {
                    if (room._room[x,y] != 0)
                    {
                        tiles = new Sprite("FloorTiles", content);
                        GameBoard[x, y] = tiles;
                        GameBoard[x, y].Position.X = (SPRITE_SIZE * x);
                        GameBoard[x, y].Position.Y = (SPRITE_SIZE * y);
                    }
                }
            }
           
        }
 
    }
}
