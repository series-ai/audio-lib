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
        public void GetAudioFile_WhenDictionaryNull_ShouldThrow()
        {
            var audioManager = new AudioManager(null, null);

            Assert.Throws<Exception>(() => audioManager.GetAudioFile(audioFileId1));
        }

        [Test]
        public void GetAudioFile_WhenDictionaryNotNullAndIdNotRegistered_ShouldThrow()
        {
            var items = new Dictionary<string, AudioFile>();

            var audioManager = new AudioManager(items, null);

            Assert.Throws<Exception>(() => audioManager.GetAudioFile(audioFileId1));
        }

        [Test]
        public void GetAudioFile_WhenDictionaryNotNullAndIdRegistered_ShouldNotThrow()
        {
            var items = new Dictionary<string, AudioFile>();
            items.Add(audioFileId1, new AudioFile());

            var audioManager = new AudioManager(items, null);

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