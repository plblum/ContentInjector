#InsertionsManager for ASP.NET MVC

<h2>Purpose</h2>

<p>The <span class=InlineCode><b><span style='font-size:12.0pt;'>InsertionsManager</span></b></span>
class lets your Partial Views and Html Helpers add their scripts and style
sheets into the appropriate areas of the page, often defined in a different
View file (such as the “master page”). Your code also can insert <span
class=GramE>meta</span> tags, hidden fields, array variables, JavaScript templates,
and any other content in predefined locations on the page. It supports the
ASP.NET Razor View Engine, but other View Engines can be expanded to support it
too.</p>

<h2>Background</h2>

<p>Partial Views and Html Helpers inject content into the page
at the location where they are defined. This is fine for adding HTML, but often
you want these tools to create something more complex, like a calendar control
or filtered textbox, which need JavaScript, both in files and inline, and style
sheet classes which do not belong side-by-side with the HTML being inserted.
They belong in specific locations in the page, often in the master page.</p>

<p>The Razor View Engine for ASP.NET MVC makes process of exposing
these elements awkward. You often have to create <span class=InlineCode><span
style='font-size:12.0pt;'>@section</span></span>
groups and hope 1) that the containing page knows to load that section and 2)
that you are not adding duplicate scripts and styles.</p>

<p>The <span class=InlineCode><span style='font-size:12.0pt;
'>InsertionsManager</span></span> class extends the
Razor View Engine to let your Views and Html Helpers register anything it wants
inserted, and handles duplicates correctly. After Razor finishes preparing the
page content, InsertionsManager will act as a post-processor to locate <b
>Insertion Points</b> throughout the page and
replace them with the content your Views and Html Helpers have registered.</p>

<a name="Example" ></a>
<h2>Example</h2>

<p>Here is a typical master page (_layout.cshtml) when using
the InsertionsManager:</p>

<pre style='background:#FFEFE6'><span style='font-size:10pt;font-family:"Lucida Console";
color:black;background:yellow'>@</span><span style='font-size:10pt;font-family:
"Lucida Console";color:blue'>using</span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>&nbsp;InsertionsManagement;</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'> </span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black;background:
yellow'>@{</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'></span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span
class=GramE><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>this</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>.InsertionsManager(</span></span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>).AddScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/jquery-1.5.1.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>);</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span class=GramE><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>this</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>.InsertionsManager(</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>).AddScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/modernizr-1.7.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>);</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span class=GramE><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>this</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>.InsertionsManager(</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>).AddStyleFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Content/Site.css&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>);</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black;background:yellow'>}</span><span style='font-size:10pt;font-family:
"Lucida Console";color:black'></span><br /><span class=GramE><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;!</span><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>DOCTYPE</span></span><span style='font-size:10pt;font-family:
"Lucida Console";color:black'>&nbsp;</span><span style='font-size:10pt;
font-family:"Lucida Console";color:red'>html</span><span style='font-size:10pt;
font-family:"Lucida Console";color:blue'>&gt;</span><span style='font-size:
10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span class=GramE><span style='font-size:10pt;
font-family:"Lucida Console";color:maroon'>html</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span class=GramE><span style='font-size:10pt;
font-family:"Lucida Console";color:maroon'>head</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='font-size:10pt;
font-family:"Lucida Console";color:blue'>&lt;</span><span style='font-size:
10pt;font-family:"Lucida Console";color:maroon'>title</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black;background:
yellow'>@</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>ViewBag.Title</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;/</span><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>title</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&gt;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'></span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&lt;</span><span
class=GramE><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>meta</span></span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:red'>charset</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>=&quot;utf-8&quot;</span><span style='font-size:10pt;font-family:
"Lucida Console";color:black'>&nbsp;</span><span style='font-size:10pt;
font-family:"Lucida Console";color:blue'>/&gt;</span><span style='font-size:
10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;</span><span class=GramE><span style='font-size:
10pt;font-family:"Lucida Console";color:darkgreen'>&lt;!--</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:darkgreen'>&nbsp;replace-with=&quot;MetaTags&quot;&nbsp;--&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;</span><span class=GramE><span style='font-size:
10pt;font-family:"Lucida Console";color:darkgreen'>&lt;!--</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:darkgreen'>&nbsp;replace-with=&quot;StyleFiles&quot;&nbsp;--&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;</span><span class=GramE><span style='font-size:
10pt;font-family:"Lucida Console";color:darkgreen'>&lt;!--</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:darkgreen'>&nbsp;replace-with=&quot;ScriptFiles&quot;&nbsp;--&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;/</span><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>head</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&gt;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'></span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black'> </span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span class=GramE><span style='font-size:10pt;
font-family:"Lucida Console";color:maroon'>body</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;</span><span class=GramE><span style='font-size:
10pt;font-family:"Lucida Console";color:darkgreen'>&lt;!--</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:darkgreen'>&nbsp;replace-with=&quot;ScriptBlocks:Upper&quot;&nbsp;--&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;&nbsp;<span class=GramE><span style='background:
yellow'>@</span>RenderBody()</span></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;</span><span class=GramE><span style='font-size:
10pt;font-family:"Lucida Console";color:darkgreen'>&lt;!--</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:darkgreen'>&nbsp;replace-with=&quot;ScriptBlocks:Lower&quot;&nbsp;--&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;/</span><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>body</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&gt;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'></span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>html</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span></pre>

<p>Understanding this code:</p>

<ul type='disc'>
 <li>The <span
     class=InlineCode><span style='font-size:12.0pt;'>@</span></span><span
     class=InlineCode><span style='font-size:12.0pt;
     color:blue'>using</span></span><span class=InlineCode><span
     style='font-size:12.0pt;'> InsertionsManagement</span></span>
     statement establishes extension classes, including the ability to use <span
     class=GramE><span class=InlineCode><span style='font-size:12.0pt;
     color:blue'>this</span></span><span
     class=InlineCode><span style='font-size:12.0pt;'>.InsertionsManager(</span></span></span><span
     class=InlineCode><span style='font-size:12.0pt;'>)</span></span>.
     You will add this to each View and Html Helper to interacts with the <span
     class=InlineCode><span style='font-size:12.0pt;'>InsertionsManager</span></span>.</li>
 <li>Calls
     made on <span class=GramE><span class=InlineCode><span style='font-size:
     12.0pt;color:blue'>this</span></span><span
     class=InlineCode><span style='font-size:12.0pt;'>.InsertionsManager(</span></span></span><span
     class=InlineCode><span style='font-size:12.0pt;'>)</span></span>
     attach the desired content. In this case, it adds two <span class=GramE>script</span>
     file URLs and a style sheet URL. If you wanted to ensure a specific order
     to your scripts, you can pass an order value as an additional parameter:

<pre style='background:#FFEFE6'><span class=GramE><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>this</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>.InsertionsManager(</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>).AddScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/jquery-1.5.1.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console"'>, 10<span
style='color:black'>);</span></span><br /><span
class=GramE><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>this</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>.InsertionsManager(</span></span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>).AddScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/modernizr-1.7.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console"'>, 20<span
style='color:black'>);</span></span></pre>

</li>
 <li>The
     comment tags containing “<span class=InlineCode><span style='font-size:
     12.0pt;'>replace-with=</span></span>” are the Insertion
     Points. Each has a name identifying the type of content it will output.</li>
</ul>

<p>Now suppose your View inserted at <span class=GramE><span
class=InlineCode><span style='font-size:12.0pt;'>@RenderBody(</span></span></span><span
class=InlineCode><span style='font-size:12.0pt;'>)</span></span>
needs to use jquery-validation. Its code should include:</p>

<pre style='background:#FFEFE6'><span style='font-size:10pt;font-family:"Lucida Console";
color:black;background:yellow'>@</span><span style='font-size:10pt;font-family:
"Lucida Console";color:blue'>using</span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>&nbsp;InsertionsManagement</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black;background:yellow'>@model&nbsp;</span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>Models.</span><span style='font-size:
10pt;font-family:"Lucida Console";color:#2B91AF'>MyModel</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'> </span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black;background:
yellow'>@{</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'></span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span
class=GramE><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>this</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>.InsertionsManager(</span></span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>).AddScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/jquery.validate.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>,&nbsp;10);</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span class=GramE><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>this</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>.InsertionsManager(</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>).AddScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/jquery.validate.unobtrusive.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>,&nbsp;11);</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black;background:yellow'>}</span><span style='font-size:10pt;font-family:
"Lucida Console";color:black'></span></pre>

<p>Here is the resulting HTML output:</p>

<pre style='background:#FFEFE6'><span class=GramE><span style='font-size:10pt;
font-family:"Lucida Console";color:blue'>&lt;!</span><span style='font-size:
10pt;font-family:"Lucida Console";color:maroon'>DOCTYPE</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:red'>html</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span class=GramE><span style='font-size:10pt;
font-family:"Lucida Console";color:maroon'>html</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span class=GramE><span style='font-size:10pt;
font-family:"Lucida Console";color:maroon'>head</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='font-size:10pt;
font-family:"Lucida Console";color:blue'>&lt;</span><span class=GramE><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>meta</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:red'>charset</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>=&quot;utf-8&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>/&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='font-size:10pt;
font-family:"Lucida Console";color:blue'>&lt;</span><span class=GramE><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>title</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>Create</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>title</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'> </span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&lt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>link</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:red'>href</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>=&quot;/Content/Site.css&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:red'>type</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>=&quot;text/css&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:red'>rel</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>=&quot;stylesheet&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>/&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'> </span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&lt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>script</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:red'>src</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>=&quot;/Scripts/jquery-1.5.1.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:red'>type</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>=&quot;text/javascript&quot;&gt;&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>script</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>script</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:red'>src</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>=&quot;/Scripts/modernizr-1.7.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:red'>type</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>=&quot;text/javascript&quot;&gt;&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>script</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>script</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:red'>src</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>=&quot;/Scripts/jquery.validate.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:red'>type</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>=&quot;text/javascript&quot;&gt;&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>script</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>script</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:red'>src</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>=&quot;/Scripts/jquery.validate.unobtrusive.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:red'>type</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>=&quot;text/javascript&quot;&gt;&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>script</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'> </span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>head</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span class=GramE><span style='font-size:10pt;
font-family:"Lucida Console";color:maroon'>body</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;</span><br /><span style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;<em>The view’s content goes here.</em></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;</span><br /><span style='font-size:10pt;font-family:"Lucida Console";color:blue'>&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>body</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;/</span><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>html</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&gt;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'></span></pre>

<p>The comment tags that did not have content have been
deleted. </p>

<p>Let’s add some script blocks, one assigned to the
"ScriptBlocks:Upper” Insertion Point and the other to the “ScriptBlocks:Lower” Insertion
Point.</p>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>
<span style='background:yellow'>@</span><span style='color:blue'>using</span>&nbsp;InsertionsManagement<br /><span style='background:yellow'>@model&nbsp;</span>Models.<span
style='color:#2B91AF'>MyModel</span><br /><span> </span><br /><span style='background:yellow'>@{</span><br /><span>   </span><span class=GramE><span
style='color:blue'>this</span>.InsertionsManager(</span>).AddScriptFile(<span
style='color:#A31515'>&quot;~/Scripts/jquery.validate.min.js&quot;</span>);<br /><span>   </span><span class=GramE><span
style='color:blue'>this</span>.InsertionsManager(</span>).AddScriptFile(<br /><span>      </span><span
style='color:#A31515'>&quot;~/Scripts/jquery.validate.unobtrusive.min.js&quot;</span>);<br /><span>   </span><span class=GramE><span
style='color:blue'>this</span>.InsertionsManager(</span>).AddScriptBlock(null, <br /><span>      </span><span
style='color:#A31515'>&quot;function <span class=GramE>test(</span>) {alert('hello');}&quot;</span><span
style='color:windowtext'>, 0, </span><span style='color:#A31515'>&quot;Upper&quot;</span>);<br /><span>   </span><span class=GramE><span
style='color:blue'>this</span>.InsertionsManager(</span>).AddScriptBlock(null, <span
style='color:#A31515'>&quot;test();&quot;</span><span style='color:windowtext'>, 0, </span><span style='color:#A31515'>&quot;Lower&quot;</span>););<br /><span style='background:yellow'>}</span><br /></pre>

<p>Here is the resulting &lt;body&gt; tag’s <span class=GramE>output:</span></p>

<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'><span style='color:blue'>&lt;</span><span class=GramE><span
style='color:maroon'>body</span></span><span style='color:blue'>&gt;</span><br />&nbsp;&nbsp;&nbsp;<span class=GramE><span style='color:blue'>function</span></span>test() {alert(<span style='color:maroon'>'hello'</span>);}<br />&nbsp;&nbsp;&nbsp;&nbsp;<em>The view’s content goes here.</em><br />&nbsp;&nbsp;&nbsp;<span class=GramE>test(</span>);<br /><span style='color:blue'>&lt;/</span><span
style='color:maroon'>body</span><span style='color:blue'>&gt;</span><span
style='color:black'></span></pre>
<h3><a name="gettingStarted">Getting started</a></h3>
<ul>
<li>Add the InsertionsManagement.dll assembly to your ASP.NET MVC web application. Requires MVC 3.0 or higher.
     (Alternatively, add the source code project and set a reference from your
     application to it.)
	  <br />
	  You can add it either using the NuGet Package Manager in Visual Studio or by going here:<br />
	  <a href="https://github.com/plblum/InsertionsManager/blob/master/Assemblies/InsertionsManager.dll?raw=true">Retrieve assembly</a>
	  </li>
<li>In Application_Start, add this code:<br />
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>ViewEngines.Engines.Clear();<br />ViewEngines.Engines.Add(new InsertionsManagement.IMRazorViewEngine());</pre>
</li>
<li>Add the customary Insertion Points to the master page, as shown in the <a href="#Example">Example above</a>.</li>
<li>Add @using InsertionsManagement to each View and Html Helper that will use this tool.</li>
<li>Call methods on this.InsertionsManager() to add content. Methods include AddScriptFile(),
AddStyleFile(), AddScriptBlock(), AddMetaTag(), AddHiddenField(),
ArrayDeclaration(), and AddPlaceholder().<br />
<em>Examples:</em><br />
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>this.InsertionsManager().AddScriptFile("~/Scripts/jquery-1.9.1.js");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>this.InsertionsManager().AddStyleFile("~/Content/StyleSheet.css");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>this.InsertionsManager().AddScriptBlock("somekey", "some javascript", 0, "Lower");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>this.InsertionsManager().AddHiddenField("codes", "AB903F");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>this.InsertionsManager().AddMetaTag("description", "about my site");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>this.InsertionsManager().AddPlaceholder("&lt;!-- xyz library used under license --&gt;");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>this.InsertionsManager().ArrayDeclaration("myVar", "my string value", 0, "Lower");<br />this.InsertionsManager().ArrayDeclaration("myVar", 100, 0, "Lower");<br />this.InsertionsManager().ArrayDeclarationAsCode("MyVar", "null", 0, "Lower");</pre>
</li>
<li>Open the <strong>Users Guide.pdf</strong> and dig in. It provides details on all described above.</li>
</ul>
