using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Classes
{
    public class Animation
    {
        private string name;
        private List<IntPtr> frames;
        private int currentFrameIndex = 0;
        private float speed = 0.5f;
        private float currentAnimationTime = 0;
        private bool isLoopEnabled;
        public IntPtr Frames => frames[currentFrameIndex];

        public Animation(string name, List<IntPtr> frames, float speed, bool isLoopEnabled)
        {
            this.name = name;
            this.frames = frames;
            this.speed = speed;
            this.isLoopEnabled = isLoopEnabled;
        }

        public void Update()
        {
            currentAnimationTime += Program.DeltaTime;

            if (currentAnimationTime >= speed)
            {
                currentFrameIndex++;
                currentAnimationTime = 0;

                if (currentFrameIndex >= frames.Count)
                {
                    if (isLoopEnabled)
                    {
                        currentFrameIndex = 0;
                    }
                    else
                    {
                        currentFrameIndex = frames.Count - 1;
                    }
                }
            }
        }
    }
}
