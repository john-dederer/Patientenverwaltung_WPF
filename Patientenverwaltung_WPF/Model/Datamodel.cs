using System;

namespace Patientenverwaltung_WPF
{
    public abstract class Datamodel
    {
        public string LastChange { get; set; }

        public void SetValues(Datamodel datamodel)
        {
            var dataModelFields = datamodel.GetType().GetFields();
            var currentModelFiels = GetType().GetFields();

            foreach (var field in dataModelFields)
            foreach (var fieldsCurrentModel in currentModelFiels)
                if (field.Name.Equals(fieldsCurrentModel.Name))
                    field.SetValue(fieldsCurrentModel, fieldsCurrentModel.Name);
        }

        public void SetLogData()
        {
            LastChange = $@"{DateTime.Now.ToLocalTime()}|{CurrentContext.GetUser().Username}";
        }
    }
}