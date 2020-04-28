/*
 * R e a d m e
 * -----------
 * VB86_PartyLights
 * By VeganBurrito86
 * Just load the script into a programmable block. Run with the Argument "Help" for help with the options.
 * 
 */
MyCommandLine T=new MyCommandLine();Dictionary<string,Action>U=new Dictionary<string,Action>(StringComparer.
OrdinalIgnoreCase);const UpdateType V=UpdateType.Trigger|UpdateType.Terminal;bool W=false;List<IMyLightingBlock>X=new List<
IMyLightingBlock>();List<IMyLightingBlock>Y=new List<IMyLightingBlock>();List<IMyLightingBlock>Z=new List<IMyLightingBlock>();int a=128;
bool c=false;bool d=false;bool e=false;bool f=false;bool h=false;float j=0;Program(){Runtime.UpdateFrequency=UpdateFrequency
.None;U["party"]=n;U["help"]=k;}void k(){string m="Always start your Argument with \"Party\".\n\n";if(T.Switch("options")
){m+="+ 'name=<string>' - searches for lights with \"<string>\" in their name.\n\n";m+=
"+ 'group=<string>' - only use lights in the group named \"<string>\".\n\n";m+="+ 'bpm=128' - set blink interval of lights to a bpm (must be an integer).\n\n";m+=
"+ 'offset=50' - set blink offset (between 0-100%, can be a decimal).\n\n";}else if(T.Switch("switches")){m+="Switches go at the end of your argument and start with a \"-\" character:\n\n";m+=
"Use \"-nocolorchange\" to keep light colors as they are and only affect blink settings.\n\n";m+="Use \"-doublerandom\" to keep randomizing light color during party.\n\n";m+=
"Use \"-othersoff\" to shut off other lights that are not part of the party.\n\n";m+="Use \"-randomblink\" to set random blink effects to all party lights.\n\n";}else{m+=
"Run with no argument for a quick demo.\n\n";m+="For a list of possible options, run again with \"-options\" or \"-switches\" after \"help\".";}Echo(m);}void n(){if
(W){Runtime.UpdateFrequency=UpdateFrequency.None;J();W=false;return;}string o="";string p="";for(var D=0;D<T.
ArgumentCount;D++){string q=T.Argument(D);if(q.Contains("=")){string[]s=q.Split('=');switch(s[0]){case"name":o=s[1];Echo(
$"Found name param: {o}");break;case"group":p=s[1];Echo($"Found group param: {p}");break;case"bpm":string t=s[1];Echo($"Found bpm param: {t}");
if(!int.TryParse(t,out a)){Echo("Invalid BPM Entered: using default or last applied bpm value.");}break;case"offset":if(!
float.TryParse(s[1],out j)){Echo("Invalid Offset Entered: setting offset to 0 instead.");j=0;}break;}}}GridTerminalSystem.
GetBlocksOfType<IMyLightingBlock>(Z,B=>B.IsSameConstructAs(Me));if(Z==null){Echo("No lights found.");N();return;}X=Z.ToList();Y=Z.
ToList();IMyBlockGroup u;List<IMyLightingBlock>S=new List<IMyLightingBlock>();if(p!=""){Echo($"Searching for group: {p}");u=
GridTerminalSystem.GetBlockGroupWithName(p);if(u==null){Echo($"Could not find a group named '{p}' on this grid.");N();return;}u.
GetBlocksOfType<IMyLightingBlock>(S);if(S.Count<=0){Echo($"Could not find any lights in the group '{p}'.");N();return;}Z=S;}if(o!=""){
Array A=Z.ToArray();foreach(IMyLightingBlock K in A){if(!K.CustomName.Contains(o)){Z.Remove(K);}if(p!=""&S.Count>0&!S.
Contains(K)){Z.Remove(K);}}if(Z.Count<=0){Echo($"Could not find any lights with name containing the string '{o}'.");N();return;}
}Echo($"{Z.Count} lights now in party mode!");foreach(IMyLightingBlock B in X){MyIni C=new MyIni();if(B.CustomData!=""){
if(!C.TryParse(B.CustomData)){Echo($"Could not parse CustomData for {B.CustomName} as MyIni. Try clearing it!");continue;}
}C.Set("party","wasEnabled",B.Enabled);C.Set("party","originalColorR",B.Color.R.ToString());C.Set("party",
"originalColorG",B.Color.G.ToString());C.Set("party","originalColorB",B.Color.B.ToString());C.Set("party","originalBlinkInterval",B.
BlinkIntervalSeconds);C.Set("party","originalBlinkLength",B.BlinkLength);C.Set("party","originalBlinkOffset",B.BlinkOffset);B.CustomData=C.
ToString();}if(T.Switch("doublerandom")){c=true;}if(T.Switch("halfoffset")){d=true;}if(T.Switch("randomblink")){f=true;}if(p!=""
||o!=""){foreach(IMyLightingBlock B in Z){Y.Remove(B);}}if(T.Switch("othersoff")){e=true;for(int D=0;D<Y.Count;D++){Y[D].
Enabled=false;}}Random E=new Random();float F=j;for(var D=0;D<Z.Count;D++){Z[D].Enabled=true;if(!h){int G=E.Next(256);int H=E.
Next(256);int I=E.Next(256);Z[D].Color=new Color(G,H,I);}if(f){Z[D].BlinkIntervalSeconds=(E.Next(1,21)/4);Z[D].BlinkLength=
50f;Z[D].BlinkOffset=E.Next(21)*5;}else{Z[D].BlinkIntervalSeconds=a/60f;Z[D].BlinkLength=50f;Z[D].BlinkOffset=F;Echo(
$"{Z[D].CustomName} offset set to {F}.");F+=j;if(F>=100){F-=100;}}}Runtime.UpdateFrequency=UpdateFrequency.Update1;W=true;}void J(){for(int D=0;D<X.Count;D++){
IMyLightingBlock K=X[D];MyIni L=new MyIni();if(!L.TryParse(K.CustomData)){Echo($"Could not reset light {K.CustomName}!");continue;}K.
Enabled=L.Get("party","wasEnabled").ToBoolean();Color M=new Color(L.Get("party","originalColorR").ToByte(),L.Get("party",
"originalColorG").ToByte(),L.Get("party","originalColorB").ToByte());K.Color=M;K.BlinkIntervalSeconds=float.Parse(L.Get("party",
"originalBlinkInterval").ToString());K.BlinkLength=float.Parse(L.Get("party","originalBlinkLength").ToString());K.BlinkOffset=float.Parse(L.Get
("party","originalBlinkOffset").ToString());}N();}void N(){Y.Clear();X.Clear();Z.Clear();c=false;d=false;e=false;f=false;
j=0;}void Main(string O,UpdateType P){if((P&V)!=0){if(T.TryParse(O)){Action Q;string R=T.Argument(0);if(R==null){Echo(
$"No command specified.");}else if(U.TryGetValue(T.Argument(0),out Q)){Q();}else{Echo($"Unknown command '{R}'");}}else{if(!W){Echo(
"No command specified, running demo mode!");j=50f;c=true;}n();}}if(0!=(P&UpdateType.Update1)){Random E=new Random();if(W&Runtime.TimeSinceLastRun.TotalSeconds>1&c
){foreach(IMyLightingBlock K in Z){int G=E.Next(256);int H=E.Next(256);int I=E.Next(256);K.Color=new Color(G,H,I);}}}}