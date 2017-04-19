﻿using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kuriimu.Contract;

namespace KuriimuTests
{
    [TestClass]
    public class ArchiveTests
    {
        const string sample_file_path = "../../../sample_files/test_files";
        const string tmp_path = "tmp";

        public static void Test<T>(string file) where T : IArchiveManager, new()
        {
            Directory.CreateDirectory(Path.Combine(sample_file_path, tmp_path));
            var path1 = Path.Combine(sample_file_path, file);
            var path2 = Path.Combine(sample_file_path, tmp_path, "test-" + Path.GetFileName(file));

            var mgr = new T();
            Assert.AreEqual(LoadResult.Success, mgr.Load(path1));
            Assert.AreEqual(SaveResult.Success, mgr.Save(path2));
            FileAssert.AreEqual(path1, path2);
            mgr.Unload();

            // delete if successful
            File.Delete(path2);
        }

        public static void HpiHpbTest(string hpiFile)
        {
            Test<archive_hpi_hpb.HpiHpbManager>(hpiFile);

            // additionally compare the hpb files
            var hpbFile = hpiFile.Remove(hpiFile.Length - 1) + "b";
            var path1 = Path.Combine(sample_file_path, hpbFile);
            var path2 = Path.Combine(sample_file_path, tmp_path, "test-" + Path.GetFileName(hpbFile));
            FileAssert.AreEqual(path1, path2);

            // delete if successful
            File.Delete(path2);
        }

        [TestMethod]
        public void HpiHpbTest1() => HpiHpbTest("mori5.hpi");

        [TestMethod]
        public void SimpleSarcTest() => Test<archive_sarc.SimpleSarcManager>("fs2.sarc");

        [TestMethod]
        public void SarcTest1() => Test<archive_sarc.SarcManager>("svn_font.sarc");

        [TestMethod]
        public void SarcTest2() => Test<archive_sarc.SarcManager>("svn_message.sarc");

        [TestMethod]
        public void SarcTest3() => Test<archive_sarc.SarcManager>("MainLang.arc");

        [TestMethod]
        public void SarcTest4() => Test<archive_sarc.SarcManager>("lovelevel.sarc");

        [TestMethod]
        public void DarcTest1() => Test<archive_darc.DARCManager>("Africa.arc");

        [TestMethod]
        public void DarcTest2() => Test<archive_darc.DARCManager>("Australia.arc");

        [TestMethod]
        public void DarcTest3() => Test<archive_darc.DARCManager>("WestAustralia.arc");

        [TestMethod]
        public void DarcTest4() => Test<archive_darc.DARCManager>("BgGtrA_L.arc");
    }
}
