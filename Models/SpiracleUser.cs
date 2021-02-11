using System;
using System.Collections.Generic;

namespace Spiracle.NETCore.Models
{
    public class SpiracleUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Dob { get; set; }
        public string CreditCard { get; set; }
        public int Cvv { get; set; }

        public SpiracleUser(object[] userData)
        {
            Id = (int) userData[0];
            Name = (string) userData[1];
            Surname = (string) userData[2];
            Dob = (DateTime) userData[3];
            CreditCard = (string) userData[4];
            Cvv = (int) userData[5];
        }

        public SpiracleUser(){}
    }
}