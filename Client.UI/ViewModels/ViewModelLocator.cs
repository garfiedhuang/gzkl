﻿using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GZKL.Client.UI.ViewsModels
{
     public class ViewModelLocator
    {
        /// <summary>
        /// 嘿巴扎嘿
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<UserViewModel>();
            SimpleIoc.Default.Register<RoleViewModel>();
            SimpleIoc.Default.Register<ConfigViewModel>();
            SimpleIoc.Default.Register<PermissionViewModel>();

            SimpleIoc.Default.Register<OrgViewModel>();
        }

        #region 实例化
        public static ViewModelLocator Instance = new Lazy<ViewModelLocator>(() =>
           Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>();
        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public UserViewModel User => ServiceLocator.Current.GetInstance<UserViewModel>();

        public RoleViewModel Role => ServiceLocator.Current.GetInstance<RoleViewModel>();
        public ConfigViewModel Config => ServiceLocator.Current.GetInstance<ConfigViewModel>();
        public PermissionViewModel Permission => ServiceLocator.Current.GetInstance<PermissionViewModel>();


        public OrgViewModel Org => ServiceLocator.Current.GetInstance<OrgViewModel>();

        #endregion

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
