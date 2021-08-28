using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ookii.Dialogs.Wpf;

namespace BatchMuxer_Sub.Modules
{
    public static class Util
    {
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
    }
}
