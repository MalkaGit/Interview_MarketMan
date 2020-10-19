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

        public Celeb(int index, string name, string role, DateTime birthDate, string gender , string imageUri, string imdb_nm)
        {
            this.Index      = index;
            this.Name       = name;
            this.Role       = role;
            this.BirthDate  = birthDate;
            this.Gender     = gender;
            this.ImageUri   = imageUri;
            this.Imdb_nm    = imdb_nm;
        }

        #endregion

        #region Public Properties

        public int          Index       { get; set; }
        public string       Name        { get; set; }

        public string       Role        { get; set; }

        public DateTime?    BirthDate   { get; set; }

        public string        Gender      { get; set; }

        public string       ImageUri    { get; set; }

        public  string       Imdb_nm { get; set; }      //eg:nm0413168 from https://www.imdb.com/name/nm0413168/?ref_=nmls_hd

        //gender

        #endregion 

    }
}
