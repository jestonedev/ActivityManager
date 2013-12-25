using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using am_classes;

namespace am_editor
{
    public partial class FormPlugins : Form
    {
        private List<PluginIncludeRule> _plugins_include_rules;
        private Language language;

        public List<PluginIncludeRule> plugins_include_rules
        {
            get
            {
                List<PluginIncludeRule> pir = new List<PluginIncludeRule>();
                foreach (string item in checkedListBoxPlugins.CheckedItems)
                    pir.Add(new PluginIncludeRule("include", item));
                return pir;
            }
            set { _plugins_include_rules = value; }
        }

        public FormPlugins(List<PluginIncludeRule> plugins_include_rules, Language language)
        {
            InitializeComponent();
            this.plugins_include_rules = plugins_include_rules;
            this.language = language;
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
                if (Plugin.IsPlugin(file))
                {
                    bool include = false;
                    foreach (PluginIncludeRule pir in _plugins_include_rules)
                    {
                        if ((pir.PluginNameMask == "*") || (pir.PluginNameMask == fi.Name))
                            include = pir.IncludeRule == "include";
                    }
                    checkedListBoxPlugins.Items.Add(fi.Name, include);
                }
            }
        }
    }
}
