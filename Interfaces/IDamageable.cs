﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Interfaces
{
    public interface IDamageable
    {
        
        float Health { get; }
        void TakeDamage(float damage);
    }
}
