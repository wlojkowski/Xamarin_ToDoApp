﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Xamarin;
namespace FireBaseApp.Models
{
    public class ToDoItem
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
