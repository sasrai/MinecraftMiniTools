using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftBorderless
{
    public class CLIOption
    {
        private string[] cmds;

        protected Dictionary<List<string>, CommandOptionType> cmdTypes = new Dictionary<List<string>, CommandOptionType>()
            {
                {new List<string>(){"/w", "/windowclass", "WindowClass", "ウィンドウクラスの指定(デフォルト値:LWJGL)"}, CommandOptionType.StringArgs},
                {new List<string>(){"/p", "/parentwindowclass", "ParentWindowClass", "親ウィンドウクラスの指定(デフォルト値:SunAwtFrame)"}, CommandOptionType.MultiStringArgs},
                {new List<string>(){"/c", "/config", "ConfigFile", "コンフィグファイルの指定(デフォルト値:wcontrol.conf)"}, CommandOptionType.StringArgs},
                {new List<string>(){"/r", "/runatstartup", "RunAtStartup", "起動時に最初に見つけたウィンドウへ自動アタッチ"}, CommandOptionType.NoArgs},
                {new List<string>(){"/l", "/automaticload", "AutomaticLoad", "初回アタッチ時に自動で設定ファイルを適用"}, CommandOptionType.NoArgs},
            };

        public enum CommandOptionType
        {
            NoArgs,
            IntArgs,
            BoolArgs,
            StringArgs,
            MultiStringArgs,
        }
        protected enum CmdMessageListIndex : int
        {
            ShortCommand = 0,
            LongCommand,
            Key,
            Information
        }

        public class CommandOption
        {
            public CommandOptionType OptionType { get; private set; }
            string value;
            List<string> multi_value;

            public CommandOption()
            {
                this.OptionType = CommandOptionType.NoArgs;
                value = null;
            }

            public CommandOption(bool value)
            {
                this.OptionType = CommandOptionType.BoolArgs;
                this.value = value.ToString();
            }

            public CommandOption(int value)
            {
                this.OptionType = CommandOptionType.IntArgs;
                this.value = value.ToString();
            }

            public CommandOption(string value)
            {
                this.OptionType = CommandOptionType.StringArgs;
                this.value = value;
            }

            public CommandOption(List<string> value)
            {
                this.OptionType = CommandOptionType.MultiStringArgs;
                this.multi_value = value;
            }

            public CommandOption(CommandOptionType type, params string[] value)
            {
                this.OptionType = type;
                if (type == CommandOptionType.MultiStringArgs)
                    this.multi_value = new List<string>(value);
                else
                    this.value = value[0];
            }

            public bool GetBoolValue()
            {
                return bool.Parse(this.value);
            }

            public int GetIntValue()
            {
                return int.Parse(this.value);
            }

            public string GetStringValue()
            {
                return this.value;
            }

            public List<string> GetMultiStringValue()
            {
                return this.multi_value;
            }

            public override string ToString()
            {
                string vstr = string.Empty;

                if (null != this.value)
                    vstr = this.value;
                else if (this.OptionType == CommandOptionType.MultiStringArgs)
                    vstr = this.multi_value.ToString();

                return "\"" + this.OptionType + ((string.Empty != vstr) ? " => " + vstr : string.Empty) + "\"";
            }
        }
        public Dictionary<string, CommandOption> Options { get; protected set; }

        public CLIOption()
        {
            this.Options = new Dictionary<string, CommandOption>();

            try
            {
                this.ParseCommmandLine();
            }
            catch (FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        protected void ParseCommmandLine()
        {
            this.cmds = System.Environment.GetCommandLineArgs();

            bool isFormatException = false;

            foreach (List<string> cmd in cmdTypes.Keys)
            {
                int i;
                for (i = 0; i < cmds.Length; i++)
                {
                    if (cmd[(int)CmdMessageListIndex.ShortCommand] == cmds[i]
                        || cmd[(int)CmdMessageListIndex.LongCommand] == cmds[i])
                    {
                        if (cmdTypes[cmd] == CommandOptionType.NoArgs)
                        {
                            this.Options.Add(cmd[(int)CmdMessageListIndex.Key], new CommandOption());
                        }
                        else if (i + 1 >= cmds.Length)
                        {
                            isFormatException = true;
                        }
                        else
                        {
                            this.Options.Add(cmd[(int)CmdMessageListIndex.Key],
                                new CommandOption(cmdTypes[cmd], cmds[i + 1]));
                        }
                    }
                }
            }

            if (isFormatException)
                throw new FormatException("コマンドライン引数の最後に値を設定する必要のあるコマンドが記述されています。");
        }
    }
}
