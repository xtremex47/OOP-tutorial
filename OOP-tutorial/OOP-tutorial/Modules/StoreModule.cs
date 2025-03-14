﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using OOP_tutorial.Classes;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Linq;

namespace OOP_tutorial.Modules
{
    public class StoreModule : ObservableObject
    {
        private ObservableCollection<Thing> Things { get; } = new ObservableCollection<Thing>();
        private ObservableCollection<Thing> FilteredThings { get; set; } = new ObservableCollection<Thing>();
        private ObservableCollection<Thing> SortedThings { get; set; } = new ObservableCollection<Thing>();
        public ObservableCollection<Thing> DisplayThings { get; set; } = new ObservableCollection<Thing>();

        public StoreModule()
        {
            AddThings();
            All = true;
        }

        public string[] ThingTypes => Enum.GetNames(typeof(ThingType));
        public string[] SMaterialTypes => Enum.GetNames(typeof(Souvenir.MaterialType));
        public string[] Rarities => Enum.GetNames(typeof(Jewelry.Rarity));
        public string[] JMaterialTypes => Enum.GetNames(typeof(Jewelry.MaterialType));

        public ICommand AddThingCommand => new RelayCommand(AddThing);

        #region Fields
        private string _selectedThingType;
        private string _selectedSMaterialTypes;
        private string _selectedRarities;
        private string _selectedJMaterialTypes;
        private string _name;
        private string _title;
        private string _author;
        private int _numberOfPages;
        private string _model;
        private string _originCountry;
        private bool _all;
        private bool _book;
        private bool _souvenir;
        private bool _jewelry;
        private string _searchThing;
        private bool _isAscending;
        private bool _groupByName;
        private bool _groupByItemId;
        private bool _groupByValue;
        private Thing _selectedThing;
        #endregion

        #region Properties

        public string SelectedThingType
        {
            get { return _selectedThingType; }
            set
            {
                _selectedThingType = value;
                RaisePropertyChanged(nameof(SelectedThingType));
            }
        }

        public string SelectedSMaterialTypes
        {
            get { return _selectedSMaterialTypes; }
            set
            {
                _selectedSMaterialTypes = value;
                RaisePropertyChanged(nameof(SelectedSMaterialTypes));
            }
        }

        public string SelectedRarities
        {
            get { return _selectedRarities; }
            set
            {
                _selectedRarities = value;
                RaisePropertyChanged(nameof(SelectedRarities));
            }
        }

        public string SelectedJMaterialTypes
        {
            get { return _selectedJMaterialTypes; }
            set
            {
                _selectedJMaterialTypes = value;
                RaisePropertyChanged(nameof(SelectedJMaterialTypes));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                RaisePropertyChanged(nameof(Author));
            }
        }

        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set
            {
                _numberOfPages = value;
                RaisePropertyChanged(nameof(NumberOfPages));
            }
        }

        public string Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged(nameof(Model));
            }
        }

        public string OriginCountry
        {
            get { return _originCountry; }
            set
            {
                _originCountry = value;
                RaisePropertyChanged(nameof(OriginCountry));
            }
        }

        public bool All
        {
            get { return _all; }
            set
            {
                _all = value;
                RaisePropertyChanged(nameof(All));
                if (_all)
                {
                    { BookFilter = false; SouvenirFilter = false; JewelryFilter = false; }
                    DisplayThings = ApplySortingAndGrouping(ApplyFiltering(Things));
                    CheckFilterSearch();
                }
                if (!All && !BookFilter && !SouvenirFilter && !JewelryFilter)
                {
                    All = true;
                }
            }
        }

        public bool BookFilter
        {
            get { return _book; }
            set
            {
                _book = value;
                RaisePropertyChanged(nameof(BookFilter));
                if (_book)
                {
                    { All = false; SouvenirFilter = false; JewelryFilter = false; }
                    DisplayThings = ApplySortingAndGrouping(ApplyFiltering(Things));
                    CheckFilterSearch();
                }
                if (!All && !BookFilter && !SouvenirFilter && !JewelryFilter)
                {
                    BookFilter = true;
                }
            }
        }

        public bool SouvenirFilter
        {
            get { return _souvenir; }
            set
            {
                _souvenir = value;
                RaisePropertyChanged(nameof(SouvenirFilter));
                if (_souvenir)
                {
                    { All = false; BookFilter = false; JewelryFilter = false; }
                    DisplayThings = ApplySortingAndGrouping(ApplyFiltering(Things));
                    CheckFilterSearch();
                }
                if (!All && !BookFilter && !SouvenirFilter && !JewelryFilter)
                {
                    SouvenirFilter = true;
                }
            }
        }

        public bool JewelryFilter
        {
            get { return _jewelry; }
            set
            {
                _jewelry = value;
                RaisePropertyChanged(nameof(JewelryFilter));
                if (_jewelry)
                {
                    { All = false; BookFilter = false; SouvenirFilter = false; }
                    DisplayThings = ApplySortingAndGrouping(ApplyFiltering(Things));
                    CheckFilterSearch();
                }
                if (!All && !BookFilter && !SouvenirFilter && !JewelryFilter)
                {
                    JewelryFilter = true;
                }
            }
        }

        public string SearchThing
        {
            get { return _searchThing; }
            set
            {
                _searchThing = value;
                RaisePropertyChanged(nameof(SearchThing));
                FilterSearch(SearchThing);
            }
        }

        public bool IsAscending
        {
            get { return _isAscending; }
            set
            {
                _isAscending = value;
                RaisePropertyChanged(nameof(IsAscending));
                DisplayThings = ApplySortingAndGrouping(FilteredThings);
                CheckFilterSearch();
                RaisePropertyChanged(nameof(DisplayThings));
            }
        }

        public bool IsGroupByName
        {
            get { return _groupByName; }
            set
            {
                _groupByName = value;
                RaisePropertyChanged(nameof(IsGroupByName));
                if (IsGroupByName)
                {
                    DisplayThings = ApplySortingAndGrouping(FilteredThings);
                    CheckFilterSearch();
                    RaisePropertyChanged(nameof(DisplayThings));
                    IsGroupByItemId = false;
                    IsGroupByValue = false;
                }
            }
        }

        public bool IsGroupByItemId
        {
            get { return _groupByItemId; }
            set
            {
                _groupByItemId = value;
                RaisePropertyChanged(nameof(IsGroupByItemId));
                if (IsGroupByItemId)
                {
                    DisplayThings = ApplySortingAndGrouping(FilteredThings);
                    CheckFilterSearch();
                    RaisePropertyChanged(nameof(DisplayThings));
                    IsGroupByName = false;
                    IsGroupByValue = false;
                }
            }
        }

        public bool IsGroupByValue
        {
            get { return _groupByValue; }
            set
            {
                _groupByValue = value;
                RaisePropertyChanged(nameof(IsGroupByValue));
                if (IsGroupByValue)
                {
                    DisplayThings = ApplySortingAndGrouping(FilteredThings);
                    CheckFilterSearch();
                    RaisePropertyChanged(nameof(DisplayThings));
                    IsGroupByItemId = false;
                    IsGroupByName = false;
                }
            }
        }

        public Thing SelectedThing
        {
            get { return _selectedThing; }
            set
            {
                _selectedThing = value;
                RaisePropertyChanged(nameof(SelectedThing));
            }
        }

        #endregion

        private enum ThingType
        {
            Book,
            Souvenir,
            Jewelry
        }

        #region Public Functions

        public void AddThing()
        {
            if (SelectedThingType == "Book")
            {
                if (Name != null && Title != null && Author != null && NumberOfPages != 0)
                {
                    AddBook(Name, Title, Author, NumberOfPages);
                    ClearProperties();
                    MessageBox.Show("Succesfully added Book.");
                }
                else
                {
                    MessageBox.Show("There are some missing values. Please check if your values.");
                }
            }
            else if (SelectedThingType == "Souvenir")
            {
                if (Name != null && Model != null && SelectedSMaterialTypes != null && OriginCountry != null)
                {
                    AddSouvenir(Name, Model, SelectedSMaterialTypes, OriginCountry);
                    ClearProperties();
                    MessageBox.Show("Succesfully added Souvenir.");
                }
                else
                {
                    MessageBox.Show("There are some missing values. Please check if your values.");
                }
            }
            else if (SelectedThingType == "Jewelry")
            {
                if (Name != null && SelectedRarities != null && SelectedJMaterialTypes != null)
                {
                    AddJewelry(Name, SelectedRarities, SelectedJMaterialTypes);
                    ClearProperties();
                    MessageBox.Show("Succesfully added Jewelry.");
                }
                else
                {
                    MessageBox.Show("There are some missing values. Please check if your values.");
                }
            }
            SwitchSortingAndGroupingFalse();
            All = true;
        }

        #endregion

        #region Private Functions

        private void AddBook(string name, string title, string author, int numberOfPages)
        {

            Book newBook = new Book(name, GetThingsCountString(), title, author, numberOfPages);
            Things.Add(newBook);
            RaisePropertyChanged(nameof(Things));
        }

        private void AddSouvenir(string name, string model, string materialType, string originCountry)
        {
            Souvenir newSouvenir = new Souvenir(name, GetThingsCountString(), model, materialType, originCountry);
            Things.Add(newSouvenir);
            RaisePropertyChanged(nameof(Things));
        }

        private void AddJewelry(string name, string rarity, string materialType)
        {
            Jewelry newJewelry = new Jewelry(name, GetThingsCountString(), rarity, materialType);
            Things.Add(newJewelry);
            RaisePropertyChanged(nameof(Things));
        }

        private void ClearProperties()
        {
            Name = null;
            Title = null;
            Author = null;
            NumberOfPages = 0;
            Model = null;
            OriginCountry = null;
            SelectedSMaterialTypes = null;
            SelectedRarities = null;
            SelectedJMaterialTypes = null;
            SelectedThingType = null;
        }

        private string GetThingsCountString()
        {
            return Things.Count.ToString();
        }

        private ObservableCollection<Thing> NoFilter()
        {
            return Things;
        }

        private ObservableCollection<Thing> FilterByBook(ObservableCollection<Thing> things)
        {
            return new ObservableCollection<Thing>(things.OfType<Book>());
        }

        private ObservableCollection<Thing> FilterBySouvenir(ObservableCollection<Thing> things)
        {
            return new ObservableCollection<Thing>(things.OfType<Souvenir>());
        }

        private ObservableCollection<Thing> FilterByJewelry(ObservableCollection<Thing> things)
        {
            return new ObservableCollection<Thing>(things.OfType<Jewelry>());
        }

        private void FilterSearch(string stringFilter)
        {
            if (stringFilter != null)
            {
                DisplayThings = new ObservableCollection<Thing>(SortedThings.Where(
                s => (s.Name.Contains(stringFilter) || s.ItemId.Contains(stringFilter) ||s.Value.ToString().Contains(stringFilter))));
            }
            else
            {
                DisplayThings = SortedThings;
            }
            RaisePropertyChanged(nameof(DisplayThings));
        }

        private void CheckFilterSearch()
        {
            if (SearchThing != null)
            {
                DisplayThings = new ObservableCollection<Thing>(SortedThings.Where(
                s => (s.Name.Contains(SearchThing) || s.ItemId.Contains(SearchThing) || s.Value.ToString().Contains(SearchThing))));
            }
            else
            {
                DisplayThings = SortedThings;
            }
            RaisePropertyChanged(nameof(DisplayThings));
        }

        private ObservableCollection<Thing> SortAscend(ObservableCollection<Thing> things)
        {
            return new ObservableCollection<Thing>(things.OrderBy(s => s.Name));
        }

        private ObservableCollection<Thing> SortDescend(ObservableCollection<Thing> things)
        {
            return new ObservableCollection<Thing>(things.OrderByDescending(s => s.Name));
        }

        private ObservableCollection<Thing> GroupByName(ObservableCollection<Thing> things)
        {
            return new ObservableCollection<Thing>(things.GroupBy(s => s.Name).SelectMany(t => t));
        }

        private ObservableCollection<Thing> GroupByValue(ObservableCollection<Thing> things)
        {
            return new ObservableCollection<Thing>(things.GroupBy(s => s.Value).SelectMany(t => t));
        }

        private ObservableCollection<Thing> GroupByItemId(ObservableCollection<Thing> things)
        {
            return new ObservableCollection<Thing>(things.GroupBy(s => s.ItemId).SelectMany(t => t).ToList());
        }

        private void SwitchSortingAndGroupingFalse()
        {
            IsAscending = false;
            IsGroupByItemId = false;
            IsGroupByName = false;
            IsGroupByValue = false;
        }

        private ObservableCollection<Thing> ApplySortingAndGrouping(ObservableCollection<Thing> collectionOfThings)
        {
            if (IsGroupByName || IsGroupByItemId || IsGroupByValue)
            {
                if (IsGroupByName) { SortedThings = GroupByName(collectionOfThings); }
                if (IsGroupByItemId) { SortedThings = GroupByItemId(collectionOfThings); }
                if (IsGroupByValue) { SortedThings = GroupByValue(collectionOfThings); }
                if (IsAscending) { SortedThings = SortAscend(SortedThings); }
                if (!IsAscending) { SortedThings = SortDescend(SortedThings); }
            } else
            {
                if (IsAscending) { SortedThings = SortAscend(collectionOfThings); }
                if (!IsAscending) { SortedThings = SortDescend(collectionOfThings); }
            }
            
            return SortedThings;
        }

        private ObservableCollection<Thing> ApplyFiltering(ObservableCollection<Thing> collectionOfThings)
        {
            if(All) { FilteredThings = NoFilter(); }
            if(BookFilter) { FilteredThings = FilterByBook(collectionOfThings); }
            if(SouvenirFilter) { FilteredThings = FilterBySouvenir(collectionOfThings); }
            if(JewelryFilter) { FilteredThings = FilterByJewelry(collectionOfThings); }
            return FilteredThings;
        }

        private void AddThings()
        {
            for (int i = 0; i < 10; i++)
            {
                Things.Add(new Book("Book-"+i.ToString(), i.ToString(), "ASTRO Things", "Carmela Manalo", 200));
                Things.Add(new Souvenir("Souvenir-"+i.ToString(), i.ToString(), "Model "+i.ToString(), Souvenir.MaterialType.Plastic.ToString(), "Philippines"));
                Things.Add(new Jewelry("Jewelry-"+i.ToString(), i.ToString(), Jewelry.Rarity.Common.ToString(), Jewelry.MaterialType.Bronze.ToString()));
            }
        }

        #endregion
    }
}
