﻿namespace AnimalsMvc.Models
{
    public class IndexViewModel
    {
        public List<Animal> Animals { get; set; }

        public IndexViewModel(List<Animal> animals)
        {
            Animals = animals;
        }
    }
}