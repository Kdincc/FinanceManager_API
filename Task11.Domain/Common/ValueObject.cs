﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Domain.Common
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetEqualityComponents();

        public bool Equals(ValueObject other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj is null or not ValueObject)
            {
                return false;
            }

            var valueObject = obj as ValueObject;

            bool isEqualityComponentsEqual = GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());

            return isEqualityComponentsEqual;
        }
    }
}
