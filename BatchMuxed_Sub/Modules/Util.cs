using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchMuxer_Sub.ProcessUtil;
using HandyControl.Tools;
using Ookii.Dialogs.Wpf;

namespace BatchMuxer_Sub.Modules
{
    public static class Util
    {
        private static readonly AppConfig Settings = GlobalDataHelper.Load<AppConfig>();
        public static DataTable CreateLanguageDt()
        {
            const string languageCodes = "aa,ab,ae,af,ak,am,an,ar,as,av,ay,az,ba,be,bg,bh,bi,bm,bn,bo,br,bs,ca,ce,ch,co,cr,cs,cu,cv,cy,da,de,dv,dz,ee,el,en,eo,es,et,eu,fa,ff,fi,fj,fo,fr,fy,ga,gd,gl,gn,gu,gv,ha,he,hi,ho,hr,ht,hu,hy,hz,ia,id,ie,ig,ii,ik,io,is,it,iu,ja,jv,ka,kg,ki,kj,kk,kl,km,kn,ko,kr,ks,ku,kv,kw,ky,la,lb,lg,li,ln,lo,lt,lu,lv,mg,mh,mi,mk,ml,mn,mr,ms,mt,my,na,nb,nd,ne,ng,nl,nn,no,nr,nv,ny,oc,oj,om,or,os,pa,pi,pl,ps,pt,qu,rm,rn,ro,ru,rw,sa,sc,sd,se,sg,si,sk,sl,sm,sn,so,sq,sr,ss,st,su,sv,sw,ta,te,tg,th,ti,tk,tl,tn,to,tr,ts,tt,tw,ty,ug,uk,ur,uz,ve,vi,vo,wa,wo,xh,yi,yo,za,zh,zu";
            const string languageNames = "Afar,Abkhazian,Avestan,Afrikaans,Akan,Amharic,Aragonese,Arabic,Assamese,Avaric,Aymara,Azerbaijani,Bashkir,Belarusian,Bulgarian,Bihari languages,Bislama,Bambara,Bengali,Tibetan,Breton,Bosnian,Catalan; Valencian,Chechen,Chamorro,Corsican,Cree,Czech,Church Slavic; Old Slavonic; Church Slavonic; Old Bulgarian; Old Church Slavonic,Chuvash,Welsh,Danish,German,Divehi; Dhivehi; Maldivian,Dzongkha,Ewe,Greek; Modern (1453-),English,Esperanto,Spanish; Castilian,Estonian,Basque,Persian,Fulah,Finnish,Fijian,Faroese,French,Western Frisian,Irish,Gaelic; Scottish Gaelic,Galician,Guarani,Gujarati,Manx,Hausa,Hebrew,Hindi,Hiri Motu,Croatian,Haitian; Haitian Creole,Hungarian,Armenian,Herero,Interlingua (International Auxiliary Language Association),Indonesian,Interlingue; Occidental,Igbo,Sichuan Yi; Nuosu,Inupiaq,Ido,Icelandic,Italian,Inuktitut,Japanese,Javanese,Georgian,Kongo,Kikuyu; Gikuyu,Kuanyama; Kwanyama,Kazakh,Kalaallisut; Greenlandic,Central Khmer,Kannada,Korean,Kanuri,Kashmiri,Kurdish,Komi,Cornish,Kirghiz; Kyrgyz,Latin,Luxembourgish; Letzeburgesch,Ganda,Limburgan; Limburger; Limburgish,Lingala,Lao,Lithuanian,Luba-Katanga,Latvian,Malagasy,Marshallese,Maori,Macedonian,Malayalam,Mongolian,Marathi,Malay,Maltese,Burmese,Nauru,BokmÃ¥l;  Norwegian; Norwegian BokmÃ¥l,Ndebele;  North; North Ndebele,Nepali,Ndonga,Dutch; Flemish,Norwegian Nynorsk; Nynorsk;  Norwegian,Norwegian,Ndebele;  South; South Ndebele,Navajo; Navaho,Chichewa; Chewa; Nyanja,Occitan (post 1500); ProvenÃ§al,Ojibwa,Oromo,Oriya,Ossetian; Ossetic,Panjabi; Punjabi,Pali,Polish,Pushto; Pashto,Portuguese,Quechua,Romansh,Rundi,Romanian; Moldavian; Moldovan,Russian,Kinyarwanda,Sanskrit,Sardinian,Sindhi,Northern Sami,Sango,Sinhala; Sinhalese,Slovak,Slovenian,Samoan,Shona,Somali,Albanian,Serbian,Swati,Sotho;  Southern,Sundanese,Swedish,Swahili,Tamil,Telugu,Tajik,Thai,Tigrinya,Turkmen,Tagalog,Tswana,Tonga (Tonga Islands),Turkish,Tsonga,Tatar,Twi,Tahitian,Uighur; Uyghur,Ukrainian,Urdu,Uzbek,Venda,Vietnamese,VolapÃ¼k,Walloon,Wolof,Xhosa,Yiddish,Yoruba,Zhuang; Chuang,Chinese,Zulu";
            string[] lCodes = Array.ConvertAll(languageCodes.Split(','), element => element.ToString());
            string[] lName = Array.ConvertAll(languageNames.Split(','), element => element.ToString());

            DataTable languages = new();
            languages.Columns.Add("Code");
            languages.Columns.Add("Name");
            for (var i = 0; i < lCodes.Length; i++)
            {
                languages.Rows.Add(lCodes[i], lName[i]);
            }

            return languages;
        }
        public static VistaFolderBrowserDialog NewFolderBrowserDialog(string description)
        {
            var folderBrowser = new VistaFolderBrowserDialog
            {
                Description = description,
                UseDescriptionForTitle = true
            };
            return folderBrowser;
        }
        public static VistaFileDialog NewFileBrowserDialog(string description, string filename)
        {
            var folderBrowser = new VistaOpenFileDialog()
            {
                CheckFileExists = true,
                Title = description,
                Filter = "mkvmerge.exe|mkvmerge.exe"
            };
            return folderBrowser;
        }

        public static TaskDialog NewTaskDialog(ref string expandedInfo)
        {
            return new TaskDialog()
            {
                Buttons = { new TaskDialogButton(ButtonType.Ok) },
                CenterParent = true,
                ExpandedInformation = expandedInfo,
                CollapsedControlText = "Show details",
                ExpandedControlText = "Hide details"
            };
        }

        public static bool RenameFiles(FileInfo[] fi)
        {
            var hasRenamed = false;
            foreach (var fl in fi)
            {
                if (fl.Extension == ".mkv") continue;
                File.Move(fl.FullName, fl.FullName.Replace(fl.Extension, ".mkv")); //caution!
                hasRenamed = true;
            }
            return hasRenamed;
        }

        public static void ProcessFile(FileInfo fi, string path, Action<object, DataReceivedEventArgs> outputHandler)
        {
            var subtitle = fi.Name.Replace(fi.Extension, ".srt");
            if (!File.Exists(Path.Combine(path, subtitle)) || File.Exists(Path.Combine(path, "muxed", fi.Name))) return;
            var output = $@"""{Path.Combine(path, "muxed", fi.Name)}""";
            var oProcess = new Process();
            var oStartInfo = new ProcessStartInfo("CMD.EXE")
            {
                WorkingDirectory = path,
                Arguments =
                    $@"/c """"{Settings.MkvMergePath}"" -o {output} --default-track 0 --language 0:{Settings.SubtitleCode} ""{subtitle}"" ""{fi.Name}""",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
            oProcess.OutputDataReceived += new DataReceivedEventHandler(outputHandler);
            oProcess.ErrorDataReceived += new DataReceivedEventHandler(outputHandler);
            oProcess.StartInfo = oStartInfo;
            oProcess.Start();
            oProcess.BeginOutputReadLine();
            oProcess.BeginErrorReadLine();
            oProcess.WaitForExit();
        }

        public static async Task<ProcessResult> ProcessFileAsync(FileInfo fi, string path, ProcessOutputReader.TextEventHandler? outputHandler = null)
        {
            var subtitle = fi.Name.Replace(fi.Extension, ".srt");
            if (!File.Exists(Path.Combine(path, subtitle)) || File.Exists(Path.Combine(path, "muxed", fi.Name))) return null;
            var output = $@"""{Path.Combine(path, "muxed", fi.Name)}""";
            ProcessStarter p = new();
            ProcessSettings ps = new()
            {
                FileName = "CMD.EXE",
                WorkingDirectory = path,
                Arguments = $@"/c """"{Settings.MkvMergePath}"" -o {output} --default-track 0 --language 0:{Settings.SubtitleCode} ""{subtitle}"" ""{fi.Name}""",
                ReadOutput = true,
                AdditionalInfo = fi.Name
            };
            if (outputHandler is null) return await p.ExecuteAsync(ps);
            ProcessOutputReader por = new();
            por.OutputChanged += outputHandler;
            return await p.ExecuteAsync(ps, por);
        }
    }
}
