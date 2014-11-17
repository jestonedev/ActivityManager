using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AMClasses;

namespace AmEditor
{
    internal partial class FormPlugins : Form
    {
        private List<PlugIncludeRule> _plugins_include_rules;

        public List<PlugIncludeRule> PluginsIncludeRules
        {
            get
            {
                List<PlugIncludeRule> pir = new List<PlugIncludeRule>();
                foreach (string item in checkedListBoxPlugins.CheckedItems)
                    pir.Add(new PlugIncludeRule("include", item));
                return pir;
            }
            set { _plugins_include_rules = value; }
        }

        public FormPlugins(List<PlugIncludeRule> PluginsIncludeRules, Language language)
        {
            InitializeComponent();
            this.PluginsIncludeRules = PluginsIncludeRules;
            Text = language.Translate("Плагины");
            button1.Text = language.Translate(button1.Text);
            button2.Text = language.Translate(button2.Text);
        }

        private void FormPlugins_Load(object sender, EventArgs e)
        {
            string plugins_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
            string[] files = Directory.GetFiles(plugins_path, "*.dll", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (PlugInfo.IsPlug(file))
                {
                    bool include = false;
                    foreach (PlugIncludeRule pir in _plugins_include_rules)
                    {
                        if ((pir.PlugNameMask == "*") || (pir.PlugNameMask == fi.Name))
                            include = pir.IncludeRule == "include";
                    }
                    checkedListBoxPlugins.Items.Add(fi.Name, include);
                }
            }
        }
    }
}
