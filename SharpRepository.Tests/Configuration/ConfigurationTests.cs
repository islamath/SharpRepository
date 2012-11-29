﻿using System;
using System.Configuration;
using NUnit.Framework;
using SharpRepository.Repository.Caching;
using SharpRepository.Repository.Configuration;
using SharpRepository.Tests.TestObjects;
using SharpRepository.InMemoryRepository;
using SharpRepository.EfRepository;
using SharpRepository.Repository;

namespace SharpRepository.Tests.Configuration
{
    

    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void InMemoryConfigurationNoParameters()
        {
            var repos = RepositoryFactory.GetInstance<Contact, string>();

            if (!(repos is InMemoryRepository<Contact, string>))
            {
                throw new Exception("Not InMemoryRepository");
            }

        }
        [Test]
        public void LoadConfigurationRepositoryByName()
        {
            var repos = RepositoryFactory.GetInstance<Contact, string>("efRepos");

            if (!(repos is EfRepository<Contact, string>))
            {
                throw new Exception("Not EfRepository");
            }

        }

        [Test]
        public void LoadConfigurationRepositoryBySectionName()
        {
            var repos = RepositoryFactory.GetInstance<Contact, string>("sharpRepository2", null);

            if (!(repos is EfRepository<Contact, string>))
            {
                throw new Exception("Not EfRepository");
            }
        }

        [Test]
        public void LoadConfigurationRepositoryBySectionAndRepositoryName()
        {
            var repos = RepositoryFactory.GetInstance<Contact, string>("sharpRepository2", "inMem");

            if (!(repos is InMemoryRepository<Contact, string>))
            {
                throw new Exception("Not EfRepository");
            }
        }

        [Test]
        public void LoadRepositoryDefaultStrategyAndOverrideNone()
        {
            var repos = RepositoryFactory.GetInstance<Contact, string>();

            if (!(repos.CachingStrategy is StandardCachingStrategy<Contact, string>))
            {
                throw new Exception("Not standard caching default");
            }

            repos = RepositoryFactory.GetInstance<Contact, string>("inMemoryNoCaching");

            if (!(repos.CachingStrategy is NoCachingStrategy<Contact, string>))
            {
                throw new Exception("Not the override of default for no caching");
            }
        }
    }
}
