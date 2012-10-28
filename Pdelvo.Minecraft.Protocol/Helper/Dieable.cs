using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pdelvo.Minecraft.Protocol.Helper
{
    public class Dieable<T>
    {
        T _item;
        public T Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }
        }

        public Dieable(T item)
        {
            Item = item;
        }

        volatile bool _isDead;

        public bool IsDead
        {
            get
            {
                return _isDead;
            }
            set
            {
                _isDead = value;
            }
        }

        public void Die(Predicate<T> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
            if (predicate(this)) IsDead = true;
        }

        public Dieable()
        {

        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification="The item can be gotten throw the property 'Item'")]
        public static implicit operator T(Dieable<T> value)
        {
            if (value == null) return default(T);
            return value.Item;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification="The Item must be specified through the constructor. A From method in not necessary.")]
        public static implicit operator Dieable<T>(T value)
        {
            return new Dieable<T>(value);
        }
    }
}
