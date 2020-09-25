﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Collections.Generic;
using System.Linq;

namespace Crudify.Internals
{
    internal class GenericControllerRouteConvention : IControllerModelConvention
    {
        private readonly IEnumerable<CrudModel> _models;

        public GenericControllerRouteConvention(IEnumerable<CrudModel> models)
        {
            _models = models;
        }

        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType)
            {
                var dtoType = controller.ControllerType.GenericTypeArguments[0];
                var entityType = controller.ControllerType.GenericTypeArguments[1];

                var model = _models.First(x
                    => x.DtoType.FullName == dtoType.FullName
                    && x.EntityType.FullName == entityType.FullName);

                if (model == null)
                    return;

                controller.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(model.Path))
                });
            }
        }
    }
}