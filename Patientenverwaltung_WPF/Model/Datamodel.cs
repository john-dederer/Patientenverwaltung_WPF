using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    public abstract class Datamodel
    {
        public string LastChange { get; set; }

        public void SetValues(Datamodel datamodel)
        {
            var dataModelFields = datamodel.GetType().GetFields();
            var currentModelFiels = this.GetType().GetFields();

            foreach(var field in dataModelFields)
            {
                foreach (var fieldsCurrentModel in currentModelFiels)
                {
                    if (field.Name.Equals(fieldsCurrentModel.Name)) field.SetValue(fieldsCurrentModel, fieldsCurrentModel.Name);
                }
            }
        }

        public void SetLogData()
        {
            LastChange = $@"{DateTime.Now.ToLocalTime()}|{CurrentContext.GetUser().Username}";
        }
    }
}
