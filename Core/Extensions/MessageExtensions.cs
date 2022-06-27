using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class MessageExtensions
    {
        #region Added
        public static string Added(string entity)
        {
            var value = $"{entity} added.";
            return value;
        }
        #endregion
        #region NotAdded
        public static string NotAdded(string entity)
        {
            var value = $"{entity} not added.";
            return value;
        }
        #endregion
        #region OneAddedToOne
        public static string OneAddedToOne(string entity, string toEntity)
        {
            var value = $"{entity} added to the {toEntity}.";
            return value;
        }
        #endregion
        #region AddOneToOneError
        public static string AddOneToOneError(string entity, string toEntity)
        {
            var value = $"An error occured while adding a {entity} to the {toEntity}!";
            return value;
        }
        #endregion
        #region OneNotAddedToOne
        public static string OneNotAddedToOne(string entity, string toEntity)
        {
            var value = $"{entity} not added to the {toEntity}!";
            return value;
        }
        #endregion
        #region AlreadyExists
        public static string AlreadyExists(string entity)
        {
            var value = $"{entity} already exists";
            return value;
        }
        #endregion
        #region NotExists
        public static string NotExists(string entity)
        {
            var value = $"{entity} doesn't exist";
            return value;
        }
        #endregion
        #region OneOrOneNotExists
        public static string OneOrOneNotExists(string entity, string andEntity)
        {
            var value = $"{entity} or {andEntity} doesn't exist!";
            return value;
        }
        #endregion
        #region Updated
        public static string Updated(string entity)
        {
            var value = $"{entity} has been updated.";
            return value;
        }
        #endregion
        #region NotUpdated
        public static string NotUpdated(string entity)
        {
            var value = $"{entity} not updated.";
            return value;
        }
        #endregion
        #region UpdateError
        public static string UpdateError(string entity)
        {
            var value = $"An error occured while updating the {entity}!";
            return value;
        }
        #endregion
        #region Deleted
        public static string Deleted(string entity)
        {
            var value = $"{entity} has been deleted.";
            return value;
        }
        #endregion
        #region DeleteError
        public static string DeleteError(string entity)
        {
            var value = $"An error occured while deleting the {entity}!";
            return value;
        }
        #endregion
        #region NotDeleted
        public static string NotDeleted(string entity)
        {
            var value = $"{entity} not deleted!";
            return value;
        }
        #endregion
        #region GetError
        public static string GetError(string entity)
        {
            var value = $"Cannot fetch the {entity}!";
            return value;
        }
        #endregion
        #region GetSuccessful
        public static string GetSuccessful(string entity)
        {
            var value = $"{entity} fetched successfully";
            return value;
        }
        #endregion
        #region NotFound
        public static string NotFound(string entity)
        {
            var value = $"{entity} not found!";
            return value;
        }
        #endregion
        #region Found
        public static string Found(string entity)
        {
            var value = $"{entity} found.";
            return value;
        }
        #endregion
        #region NameIsSame
        public static string NameIsSame(string entity)
        {
            var value = $"You entered your previous {entity}.";
            return value;
        }
        #endregion
        #region OneInOne
        public static string OneInOne(string inOne, string one)
        {
            var value = $"{inOne} contains this {one}.";
            return value;
        }
        #endregion
        #region OneNotInOne
        public static string OneNotInOne(string inOne, string one)
        {
            var value = $"{inOne} doesn't contain this {one}!";
            return value;
        }
        #endregion
        #region AreNotEqual
        public static string AreNotEqual(string ones)
        {
            var value = $"{ones} are not equal!";
            return value;
        }
        #endregion
        #region Error
        public static string Error(string entity)
        {
            var value = $"{entity} error !";
            return value;
        }
        #endregion
        #region OneHasNoOne
        public static string OneHasNoOne(string one, string hasOne)
        {
            var value = $"{one} doesn't have any {hasOne}!";
            return value;
        }
        #endregion
        #region Overloading
        public static string Overloading(string model)
        {
            var value = $"{model} overloading ! ";
            return value;
        }
        #endregion

    }
}
