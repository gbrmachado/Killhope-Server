using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.Serialization;
using Killhope.Plugins.Rocks.Domain.Application;

namespace Killhope.Plugins.Rocks.Domain
{
    /// <summary>
    /// The content of a section of data inside the rock
    /// </summary>
    public class Content
    {
        public string Title { get; set; }
        public string Data { get; set; }

        [JsonExtensionData]
        private Dictionary<string, JToken> extra { get; set; }
    }

    public class Rock
    {
        public string UniqueId { get; set; }
        public List<string> Images { get; set; }
        public List<int> GalleryImages { get; set; }
        public string VideoPath { get; set; }
        public string Title { get; set; }
        public string Formula { get; set; }
        public List<Content> Content { get; set; }

        [JsonExtensionData]
        private Dictionary<string, JToken> extra { get; set; }

        internal ValidationResult Validate()
        {
            ValidationResult v = new ValidationResult();
            v.AddIfNullOrEmpty(UniqueId, "UniqueID");
            v.AddIfNull(Images, "Images");
            v.AddIfNull(GalleryImages, "GalleryImages");
            v.AddIfNullOrEmpty(Title, "UniqueID");
            v.AddIfNull(Formula, "Formula");
            v.AddIfEmpty(Content, "Content must have at least 1 element.");

            return v;

        }
    }

    public class RockList
    {
        public static RockList FromJson(string JSON)
        {
            return JsonConvert.DeserializeObject<RockList>(JSON);
        }

        public static string ToJson(RockList rockList)
        {
            return JsonConvert.SerializeObject(rockList);
        }

        public RockList()
        {
            Rocks = new List<Rock>();
        }

        public List<Rock> Rocks { get; set; }

        /// <summary>
        /// The number of Rocks in the collection.
        /// </summary>
        public int Count { get { return Rocks.Count; } }

        /// <summary>
        /// Performs a deep clone of the specified rock.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal Rock Clone(int index)
        {
            Rock obj = Rocks[index];

            using (var ms = new MemoryStream())
            {
                DataContractSerializer xs = new DataContractSerializer(typeof(Rock));
                xs.WriteObject(ms, obj);
                ms.Position = 0;
                var deserialised = xs.ReadObject(ms);
                return (Rock)deserialised;
            }


        }

        internal int IndexOf(string selectedID)
        {
            Rock r = Rocks.FirstOrDefault((a) => a.UniqueId == selectedID);
            if (r == null)
                return -1;

            return Rocks.IndexOf(r);
        }

        internal void Add(Rock rock)
        {
            if(rock == null)
                throw new ArgumentNullException("rock");

            if (IndexOf(rock.UniqueId) != -1)
                throw new ArgumentException("Rock already exists in collection.");

            this.Rocks.Add(rock);
        }

        internal void Replace(string idToUpdate, Rock rock)
        {
            if (rock == null)
                throw new ArgumentNullException("rock");

            int index = IndexOf(idToUpdate); 
            if (index == -1)
                throw new ArgumentException("Supplied ID not found in collection.");

            //Remove the rock from the collection.

            Rocks.RemoveAt(index);

            //Verify the (possibly changed) ID not in collection already
            if (IndexOf(rock.UniqueId) != -1)
                throw new ArgumentException("A rock with the specified ID already exists in the collection.");


            //Insert the new rock at the same place.
            Rocks.Insert(index, rock);
        }

        /// <summary>
        /// Deletes the rock with the selected ID and returns the previous element in the collection (or null if there are no more elements in the collection to edit).
        /// </summary>
        /// <param name="selectedID">The Unique ID of the rock to delete.</param>
        /// <returns>The next rock to be deleted (or 0 is no more rocks).</returns>
        internal Rock Delete(string selectedID)
        {
            if (selectedID == null)
                throw new ArgumentNullException("selectedID");
            
            int index = this.IndexOf(selectedID);

            if (index < 0)
                throw new ArgumentException("specified item not found in collection.");

            Rocks.RemoveAt(index);

            int nextID = Math.Min(Rocks.Count - 1, index);

            //if the count is 0, this will be -1, we want to return that there is no "Next" rock.
            return nextID < 0 ? null : Rocks[nextID];
        }

        internal int IndexOf(Rock selected)
        {
            if (selected == null)
                return -1;

            return this.IndexOf(selected.UniqueId);
        }
    }
}
