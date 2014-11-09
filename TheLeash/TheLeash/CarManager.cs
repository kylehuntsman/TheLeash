using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace TheLeash
{
    class CarManager
    {
        private List<Car> cars;
        private List<Car> removedCars;
        private List<Animation> animations;
        private List<Point> spawnLocations;
        private Random random = new Random();

        private SoundEffect soundEffect;
        private List<SoundEffect> soundEffects;

        public CarManager()
        {
            cars = new List<Car>();
            removedCars = new List<Car>();
            animations = new List<Animation>();
            spawnLocations = new List<Point>();
            soundEffects = new List<SoundEffect>();

            Point spawnA = new Point(0, 301);
            Point spawnB = new Point(1080, 500);
            Point spawnC = new Point(1080, 200);
            Point spawnD = new Point(0, 700);
            Point spawnE = new Point(0, 100);
            spawnLocations.Add(spawnA);
            spawnLocations.Add(spawnB);
            spawnLocations.Add(spawnC);
            spawnLocations.Add(spawnD);
            spawnLocations.Add(spawnE);
        }

        public virtual void LoadContent(ContentManager content)
        {
            Animation carAnim = new Animation(content.Load<Texture2D>(@"Images/TestImages/TestCar_Anim"),
               new Point(31, 25), new Point(0, 0), new Point(1, 2), 600);
            animations.Add(carAnim);

            soundEffects.Add(content.Load<SoundEffect>(@"Audio/carSound1"));
            //soundEffects.Add(content.Load<SoundEffect>(@"Audio/carSound2"));
            //soundEffects.Add(content.Load<SoundEffect>(@"Audio/carSound3"));
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Car car in this.cars)
            {
                car.Update(gameTime);
                if (car.X + car.Bounds.Width < 0 || car.X > 1080)
                {
                    removedCars.Add(car);
                }
            }
            foreach (Car car in this.removedCars)
            {
                cars.Remove(car);
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
            int animationIndex = random.Next(0, animations.Count);
            int spawnIndex = random.Next(0, spawnLocations.Count);
            int soundIndex = random.Next(0, soundEffects.Count);

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

            newCar.SoundEffect = soundEffects[soundIndex];

            return newCar;
        }

        public int CarCount()
        {
            return cars.Count;
        }
    }
}
