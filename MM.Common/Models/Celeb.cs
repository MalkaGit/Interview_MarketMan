using System;
using System.Collections.Generic;
using System.Text;

namespace MM.Common.Models
{
    public class Celeb
    {
        #region Public constuctor

        public Celeb()
        {
        }

        public Celeb(int index, string name, string role, DateTime birthDate, string gender , string imageUri)
        {
            this.Index      = index;
            this.Name       = name;
            this.Role       = role;
            this.BirthDate  = birthDate;
            this.Gender     = gender;
            this.ImageUri   = imageUri;
        }

        #endregion

        #region Public Properties

        public int          Index       { get; set; }
        public string       Name        { get; set; }

        public string       Role        { get; set; }

        public DateTime     BirthDate   { get; set; }

        public string        Gender      { get; set; }

        public string       ImageUri    { get; set; }

        //gender

        #endregion 

    }
}
