﻿using System;
using AppGet.FlightPlans;
using AppGet.InstalledPackages;
using AppGet.Installers;
using AppGet.Options;
using AppGet.PackageRepository;

namespace AppGet.Commands.Uninstall
{
    public class UninstallCommandHandler : ICommandHandler
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IFlightPlanService _flightPlanService;
        private readonly IUninstallService _uninstallService;
        private readonly IInventoryManager _inventoryManager;

        public UninstallCommandHandler(IPackageRepository packageRepository,
            IFlightPlanService flightPlanService, IUninstallService uninstallService, IInventoryManager inventoryManager)
        {
            _packageRepository = packageRepository;
            _flightPlanService = flightPlanService;
            _uninstallService = uninstallService;
            _inventoryManager = inventoryManager;
        }

        public bool CanExecute(AppGetOption packageCommandOptions)
        {
            return packageCommandOptions is UninstallOptions;
        }

        public void Execute(AppGetOption packageCommandOptions)
        {
            var uninstallOptions = (UninstallOptions)packageCommandOptions;

            if (!_inventoryManager.IsInstalled(uninstallOptions.PackageId))
            {
                throw new PackageNotInstalledException(uninstallOptions.PackageId);
            }

            var package = _packageRepository.GetLatest(uninstallOptions.PackageId);
            if (package == null)
            {
                throw new PackageNotFoundException(uninstallOptions.PackageId);
            }


            var flightPlan = _flightPlanService.LoadFlightPlan(package);

            _uninstallService.Uninstall(flightPlan, uninstallOptions);

            _inventoryManager.RemoveInstalledPackage(package);
        }
    }
}