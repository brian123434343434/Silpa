using System;
using System.Collections.Generic;
using System.Text;

namespace Silpa.Workflow.Entidades
{
    public class ActividadInfo
    {
        private int? activityInstance;
        private int? activityId;
        private int? participantId;
        private string actividadDescripcion;
        private int? actividadSilpaId;
        private string actividadSilpaDescripcion;

        public int? ActivityInstance
        {
            get {return activityInstance;}
            set {activityInstance = value;}
        }
        public int? ActivityId
        {
            get { return activityId; }
            set { activityId = value; }
        }

        public int? ParticipanId
        {
            get { return participantId; }
            set { participantId = value; }
        }
        public string ActividadDescripcion
        {
            get { return actividadDescripcion; }
            set { actividadDescripcion = value; }
        }
        public int? ActividadSilpaId
        {
            get { return actividadSilpaId; }
            set { actividadSilpaId = value; }
        }
        public string ActividadSilpaDescripcion
        {
            get { return actividadSilpaDescripcion; }
            set { actividadSilpaDescripcion = value; }
        }

        public ActividadInfo(int? activityInstance, int? activityId, string actividadDescripcion, int? actividadSilpaId, string actividadSilpaDescripcion,string participantId)
        {
            this.ActivityInstance = activityInstance;
            this.ActivityId = activityId;
            this.ActividadDescripcion = actividadDescripcion;
            this.ActividadSilpaId = actividadSilpaId;
            this.ActividadSilpaDescripcion = actividadSilpaDescripcion;
            if (participantId=="")
                this.ParticipanId = null;
            else
                this.ParticipanId = int.Parse(participantId);

        }
    }
}
