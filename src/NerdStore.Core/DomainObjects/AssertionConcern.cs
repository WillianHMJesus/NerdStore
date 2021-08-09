using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NerdStore.Core.DomainObjects
{
    public class AssertionConcern
    {
        public static void ValidateEquals(object objA, object objB, string message)
        {
            if (objA.Equals(objB))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateNotEquals(object objA, object objB, string message)
        {
            if (!objA.Equals(objB))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateCharacters(string value, int maximum, string message)
        {
            var length = value.Trim().Length;
            if (length > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateCharacters(string value, int minimum, int maximum, string message)
        {
            var length = value.Trim().Length;
            if (length < minimum || length > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateExpression(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);
            if (!regex.IsMatch(value))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateNullOrEmpty(string value, string message)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumAndMaximum(long value, long minimum, long maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumAndMaximum(int value, int minimum, int maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumAndMaximum(short value, short minimum, short maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumAndMaximum(decimal value, decimal minimum, decimal maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumAndMaximum(double value, double minimum, double maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimumAndMaximum(float value, float minimum, float maximum, string message)
        {
            if (value < minimum || value > maximum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLessThanEqualMinimum(long value, long minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLessThanEqualMinimum(int value, int minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLessThanEqualMinimum(short value, short minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLessThanEqualMinimum(decimal value, decimal minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLessThanEqualMinimum(double value, double minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateLessThanEqualMinimum(float value, float minimum, string message)
        {
            if (value <= minimum)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateFalse(bool value, string message)
        {
            if (!value)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateTrue(bool value, string message)
        {
            if (value)
            {
                throw new DomainException(message);
            }
        }
    }
}
