using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class CarManager
    {
        private List<Car> cars;
        private List<Animation> animations;
        private List<Point> spawnLocations;

        public CarManager()
        {
            cars = new List<Car>();
            animations = new List<Animation>();
            spawnLocations = new List<Point>();
        }

        public virtual void LoadContent(ContentManager content)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Car car in this.cars)
            {
                car.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Car car in this.cars)
            {
                car.Draw(gameTime, spriteBatch);
            }
        }

        public void AddCar(int numberOfCars = 1)
        {
            if (numberOfCars <= 0)
                return;
            for (int index = 0; index < numberOfCars; index++)
            {
                cars.Add(GenerateCar());
            }
        }

        private Car GenerateCar()
        {
            Random random = new Random();
            int animationIndex = random.Next(0, animations.Count - 1);
            int spawnIndex = random.Next(0, spawnLocations.Count - 1);
            float speed = (float)random.Next(10, 30);
            if (spawnLocations[spawnIndex].X <= 0)
            {
                speed *= 1;
            }
            else
            {
                speed *= -1;
            }
            Car newCar = new Car(animations[animationIndex],
                (float)spawnLocations[spawnIndex].X,
                (float)spawnLocations[spawnIndex].Y,
                speed);
            return newCar;
        }
    }
}
