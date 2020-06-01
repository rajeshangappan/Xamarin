using AutoMapper;
using System.Collections.Generic;
using XamSample.Models;
using XamSample.Models.DAO;

namespace XamSample.Automap
{
    /// <summary>
    /// Defines the <see cref="ProfileMapper" />.
    /// </summary>
    public class ProfileMapper : Profile
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileMapper"/> class.
        /// </summary>
        public ProfileMapper()
        {
            CreateMap<Product, ProductDAO>();
            CreateMap<List<Product>, List<ProductDAO>>();
        }

        #endregion
    }
}
