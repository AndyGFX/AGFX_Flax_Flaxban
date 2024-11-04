using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game.Sokoban
{
    /// <summary>
    /// LevelBuilder Script.
    /// </summary>
    public class LevelBuilder : Script
    {
        public List<TChest> chests = new List<TChest>();
        private Color[] pixels;
        private GridCell[,] grid = new GridCell[0, 0];
        private Actor self;
        private Random random = new Random();

        public LevelDefinition definition;
        public Vector2 homePosition = new Vector2(0, 0);
        /// <inheritdoc/>
        public override void OnStart() 
        {            
            this.pixels = new Color[this.definition.map.Height * this.definition.map.Width];
            this.grid = new GridCell[this.definition.map.Height, this.definition.map.Width];
            this.definition.map.GetPixels(out this.pixels);
            this.self = Actor;
            
        }

        /// <inheritdoc/>
        public override void OnEnable() { }

        /// <inheritdoc/>
        public override void OnDisable() { }

        /// <inheritdoc/>
        public override void OnUpdate() { }

        

        private Color GetPixelColor(int x, int y)
        {
            Color color = this.pixels[y * this.definition.map.Width + x];
            //this.grid = new GridCell[this.definition.map.Width, this.definition.map.Height];
            return color;

        }
        
        public void BuildLevel()        
        {
            Debug.Log("Building Level");
            
            
            for (int y = 0; y < this.definition.map.Height; y++)
            {
                for (int x = 0; x < this.definition.map.Width; x++)
                {

                    Color c = this.GetPixelColor(x, y);
                    

                    if (c == this.definition.colorFloor)
                    {
                    
                        this.grid[x, y] = new GridCell(eCellType.FLOOR);    
                        this.grid[x, y].collision = false;
                        Actor floor = PrefabManager.SpawnPrefab(this.definition.prefabFloor, this.self);
                        
                        floor.Position = new Float3(y * this.definition.gridCellSpacing, 0, x * this.definition.gridCellSpacing);
                    }
                    
                    if (c == this.definition.colorTarget)
                    {
                        this.grid[x, y] = new GridCell(eCellType.TARGET);
                        this.grid[x, y].collision = false;
                        Actor target = PrefabManager.SpawnPrefab(this.definition.prefabTarget, this.self);
                        target.Position = new Float3(y * this.definition.gridCellSpacing, 0, x * this.definition.gridCellSpacing);
                    }
                    
                    if (c == this.definition.colorChest)
                    {
                        this.grid[x, y] = new GridCell(eCellType.CHEST);
                        this.grid[x, y].collision = false;
                        Actor block = PrefabManager.SpawnPrefab(this.definition.prefabChest, this.self);
                        block.Position = new Float3(y * this.definition.gridCellSpacing, 0, x * this.definition.gridCellSpacing);
                        TChest chest = new TChest();
                        chest.actor = block;
                        chest.gridPosition = new Vector2(x, y);
                        chest.worldPosition = block.Position;
                        this.chests.Add(chest);
                    }

                    if (c == this.definition.colorWall)
                    {
                        this.grid[x, y] = new GridCell(eCellType.WALL);
                        Actor wall = PrefabManager.SpawnPrefab(this.definition.prefabWall, this.self);
                        this.grid[x, y].collision = true;
                        wall.Position = new Float3(y * this.definition.gridCellSpacing, 0, x * this.definition.gridCellSpacing);
                    }
                    
                    if (c == this.definition.colorHome)
                    {
                        this.grid[x, y] = new GridCell(eCellType.HOME);
                        this.grid[x, y].collision = false;
                        Actor floor = PrefabManager.SpawnPrefab(this.definition.prefabHome, this.self);
                        floor.Position = new Float3(y * this.definition.gridCellSpacing, 0, x * this.definition.gridCellSpacing);
                        
                        this.homePosition.X = x;
                        this.homePosition.Y = y;

                        Debug.Log(this.homePosition);
                    }

                    
                    if ((c == this.definition.colorOuterZone) && (this.definition.prefabOuterZone))
                    {
                        this.grid[x, y] = new GridCell(eCellType.FLOOR);
                        this.grid[x, y].collision = true;
                        Actor wall = PrefabManager.SpawnPrefab(this.definition.prefabOuterZone, this.self);
                        
                        float yo = 10f * (RandomUtil.Rand() * 5f);
                        wall.Position = new Float3(y * this.definition.gridCellSpacing, 0, x * this.definition.gridCellSpacing);
                    }
                   

                }
            }

            

        }

        public eCellType GetCellType(int x, int y)
        {
            return this.grid[x, y].cellType;
        }

        public eCellType GetCellType(Vector2 position)
        {
            return this.grid[(int)position.X, (int)position.Y].cellType;
        }    

        public bool IsObstacle(Vector2 position)    
        {
            return this.grid[(int)position.X, (int)position.Y].collision;
        }

        public TChest GetChest(Vector2 position)
        {
            foreach (TChest chest in this.chests)
            {
                if (chest.gridPosition == position)
                {
                    return chest;
                }
            }
            return null;
        }   
    }
}