using UnityEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Padoru.Audio.Tests
{
    [TestFixture]
    public class AudioManagerTests : MonoBehaviour
    {
        private string audioFileId1 = "Audio1";

        [Test]
        public void GetAudioFile_WhenDatabseNull_ShouldThrow()
        {
            var audioManager = new AudioManager(null, null);

            Assert.Throws<Exception>(() => audioManager.GetAudioFile(audioFileId1));
        }

        [Test]
        public void GetAudioFile_WhenDatabseNotNullAndDatabaseItemsCollectionNull_ShouldThrow()
        {
            var database = new FakeAudioManagerDatabase(null);
            var audioManager = new AudioManager(database, null);

            Assert.Throws<Exception>(() => audioManager.GetAudioFile(audioFileId1));
        }

        [Test]
        public void GetAudioFile_WhenDatabseNotNullAndDatabaseItemsCollectionNotNullAndIdNotRegistered_ShouldThrow()
        {
            var items = new Dictionary<string, AudioFile>();

            var database = new FakeAudioManagerDatabase(items);
            var audioManager = new AudioManager(database, null);

            Assert.Throws<Exception>(() => audioManager.GetAudioFile(audioFileId1));
        }

        [Test]
        public void GetAudioFile_WhenDatabseNotNullAndDatabaseItemsCollectionNotNullAndIdRegistered_ShouldNotThrow()
        {
            var items = new Dictionary<string, AudioFile>();
            items.Add(audioFileId1, new AudioFile());

            var database = new FakeAudioManagerDatabase(items);
            var audioManager = new AudioManager(database, null);

            Assert.DoesNotThrow(() => audioManager.GetAudioFile(audioFileId1));
        }

        [Test]
        public void GetAudioSource_WhenRetrieverNull_ShouldThrow()
        {
            var audioManager = new AudioManager(null, null);

            Assert.Throws<Exception>(() => audioManager.GetAudioSource());
        }

        [Test]
        public void GetAudioSource_WhenRetrieverNotNull_ShouldNotThrow()
        {
            var retriever = new FakeAudioSourceRetriever();
            var audioManager = new AudioManager(null, retriever);

            Assert.DoesNotThrow(() => audioManager.GetAudioSource());
        }

        [Test]
        public void ReturnAudioSource_WhenRetrieverNullAndAudioSourceNotNull_ShouldThrow()
        {
            var audioManager = new AudioManager(null, null);

            Assert.Throws<Exception>(() => audioManager.ReturnAudioSource(new AudioSource()));
        }

        [Test]
        public void ReturnAudioSource_WhenRetrieverNotNullAndAudioSourceNull_ShouldThrow()
        {
            var retriever = new FakeAudioSourceRetriever();
            var audioManager = new AudioManager(null, retriever);

            Assert.Throws<Exception>(() => audioManager.ReturnAudioSource(null));
        }

        [Test]
        public void ReturnAudioSource_WhenRetrieverNotNullAndAudioSourceNotNull_ShouldNotThrow()
        {
            var retriever = new FakeAudioSourceRetriever();
            var audioManager = new AudioManager(null, retriever);

            Assert.Throws<Exception>(() => audioManager.ReturnAudioSource(new AudioSource()));
        }
    }
}