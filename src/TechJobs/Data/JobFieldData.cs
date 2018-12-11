using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechJobs.Models;

namespace TechJobs.Data
{
    public class JobFieldData<TField> where TField : JobField, new()
    {

        private List<TField> _allFields = new List<TField>();

        private void Add(TField field)
        {
            _allFields.Add(field);
        }

        public List<TField> ToList()
        {
            return _allFields;
        }

        public TField Find(int id)
        {
            var results = from field in _allFields
                          where field.ID == id
                          select field;

            return results.Single();
        }

        internal TField AddUnique(string fieldValue)
        {

            var results = from field in _allFields
                          where field.Value.Equals(fieldValue)
                          select field;

            TField theField;

            if (!results.Any())
            {
                theField = new TField {Value = fieldValue};

                Add(theField);
            }
            else
            {
                theField = results.Single();
            }

            return theField;

        }

    }
}
