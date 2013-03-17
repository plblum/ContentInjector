#Content Injector for ASP.NET MVC


<h2>Purpose</h2>
<p>Content Injector lets you build smarter Views, Partial Views, and Html Helpers which dictate the scripts and style sheets they need. They can safely inject content into the page in areas already rendered, such as the &lt;head&gt; tag, where style sheets and some scripts should be located. Content Injector will also prevent outputting duplicate tags that load your style sheets and scripts.</p>
<p>Your Views and Helpers also can inject hidden fields, array declarations, &lt;meta&gt; tags, JavaScript templates, and any other content to predefined locations on the page. </p>

<p>It compliments other tools, like <a href="http://aspnetoptimization.codeplex.com/" target="_blank">Web Optimization Framework</a>, by letting them collect and manage data, while letting Content Injector inject the data into the page.</p>
<p>It supports the ASP.NET Razor View Engine, and is customizable.</p>

<h2>Background</h2>

<p>Partial Views and Html Helpers inject content into the page at the location where they are defined. This is fine for adding HTML, but often you want these tools to create something more complex, like a calendar control or filtered textbox, which need JavaScript, both in files and inline, and style sheet classes which do not belong side-by-side with the HTML being injected. They belong in specific locations in the page, often in the master page.</p>
<p>The Razor View Engine for ASP.NET MVC does not make this easy.</p>
<ul type='disc'>
<li>You often have to create @section groups and hope that the containing page knows to load that section. It's even trickier when working within an Html Helper.</li>
<li>The same content can be inserted multiple times, such as two Views which add the same &lt;script&gt; tag.</li>
<li>Razor's one-pass rendering engine prevents inserting your content in areas of the page that were already rendered. For example, if your View needs a &lt;link&gt; tag in the &lt;head&gt;, it has to know about that before it does its rendering.</li>
</ul>
<p>Microsoft's Web Optimization framework enhances the collection of scripts and style sheets, but still suffers from the above problems.</p>
<p>Content Injector extends the Razor View Engine to let your Views and Html Helpers register anything they want injected into the page, and handles duplicates correctly. After Razor finishes preparing the page content, Content Injector will act as a post-processor to locate <b>Injection Points</b> throughout the page and replace them with the content your Views and Html Helpers have registered.</p>

<a name="Example" ></a>
<h2>Example</h2>

<p>Here is a typical master page (_layout.cshtml) when using
the Content Injector:</p>

<pre style='background:#FFEFE6'><span
style='font-size:10pt;font-family:"Lucida Console";color:black;background:
yellow'>@{</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'></span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span
class=GramE><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>Injector</span></span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>.ScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/jquery-1.5.1.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>);</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span class=GramE><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>Injector</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>.ScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/modernizr-1.7.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>);</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span class=GramE><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>Injector</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>.StyleFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Content/Site.css&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>;</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
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
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;<span 
      style="background-color: #FFFF00;">@</span>Injector.InjectionPoint(</span><span
style='font-size:10pt;font-family:"Lucida Console";color: #A31515;'>&quot;MetaTag&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>)<br />&nbsp;&nbsp;&nbsp;&nbsp;<span 
      style="background-color: #FFFF00;">@</span>Injector.InjectionPoint(</span><span
style='font-size:10pt;font-family:"Lucida Console";color: #A31515;'>&quot;StyleFiles&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>)<br />&nbsp;&nbsp;&nbsp;&nbsp;<span 
      style="background-color: #FFFF00;">@</span>Injector.InjectionPoint(</span><span
style='font-size:10pt;font-family:"Lucida Console";color: #A31515;'>&quot;ScriptFiles&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>)</span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;/</span><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>head</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&gt;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'></span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>    </span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span class=GramE><span style='font-size:10pt;
font-family:"Lucida Console";color:maroon'>body</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;<span 
      style="background-color: #FFFF00;">@</span>Injector.InjectionPoint(</span><span
style='font-size:10pt;font-family:"Lucida Console";color: #A31515;'>&quot;ScriptBlocks&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";'>, </span><span
style='font-size:10pt;font-family:"Lucida Console";color: #A31515;'>&quot;Upper&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>)<br /></span>&nbsp;&nbsp;&nbsp;&nbsp;<span class=GramE><span style='background:
yellow'>@</span>RenderBody()</span></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;<span 
      style="background-color: #FFFF00;">@</span>Injector.InjectionPoint(</span><span
style='font-size:10pt;font-family:"Lucida Console";color: #A31515;'>&quot;ScriptBlocks&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";'>, </span><span
style='font-size:10pt;font-family:"Lucida Console";color: #A31515;'>&quot;Lower&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>)<br /></span></span><span style='font-size:10pt;font-family:"Lucida Console";
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
<li>The <span class=InlineCode><span style='font-size:12.0pt;'>Injector</span></span> element is property on your page that hosts the methods you use to interact with Content Injector. In this case, the user has added two scripts files URLs and a style sheet URL. If you wanted to ensure a specific order to your scripts, you can pass an order value as an additional parameter:<br />
<pre style='background:#FFEFE6'><span class=GramE><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>Injector</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>.ScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/jquery-1.5.1.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console"'>, 10<span
style='color:black'>);</span></span><br /><span
class=GramE><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>Injector</span></span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>.ScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/modernizr-1.7.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console"'>, 20<span
style='color:black'>);</span></span></pre>
</li>
<li>The <span class=InlineCode><span style='font-size:12.0pt;'>Injector.InjectionPoint()</span></span> method marks the location for your content associated with the name given. This call does not output your content. Instead, it just leaves a marker for the Content Injector's post processor to cleanup.</li>
<li>There are two Injection Points for “ScriptBlocks”. Each has been given a unique grouping name as the second parameter. Any injection of script blocks must use the same group name to be injected into the page. For script blocks, there are typically blocks before and after the HTML the scripts operate upon. We've given them group names of “Upper” and “Lower” here.</li>
</ul>

<p>Now suppose your View inserted at <span class=GramE><span
class=InlineCode><span style='font-size:12.0pt;'>@RenderBody(</span></span></span><span
class=InlineCode><span style='font-size:12.0pt;'>)</span></span>
needs to use jquery-validation. Its code should include:</p>

<pre style='background:#FFEFE6'><span style='font-size:10pt;font-family:"Lucida Console";
color:black;background:yellow'>@model&nbsp;</span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>Models.</span><span style='font-size:
10pt;font-family:"Lucida Console";color:#2B91AF'>MyModel</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>    </span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black;background:
yellow'>@{</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'></span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;</span><span
class=GramE><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>Injector</span></span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>.ScriptFile(</span><span
style='font-size:10pt;font-family:"Lucida Console";color:#A31515'>&quot;~/Scripts/jquery.validate.min.js&quot;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>,&nbsp;10);</span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;<span
class=GramE><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>Injector</span></span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>.ScriptFile</span>(</span><span
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
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>title</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'>Create</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>title</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>    </span><br /><span
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
color:black'>    </span><br /><span
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
color:black'>    </span><br /><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>head</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;</span><span class=GramE><span style='font-size:10pt;
font-family:"Lucida Console";color:maroon'>body</span></span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;</span><br /><span style='font-size:10pt;font-family:"Lucida Console";color:black'>&nbsp;&nbsp;&nbsp;&nbsp;<em>The view's content goes here.</em></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:black'>&nbsp;&nbsp;&nbsp;</span><br /><span style='font-size:10pt;font-family:"Lucida Console";color:blue'>&lt;/</span><span
style='font-size:10pt;font-family:"Lucida Console";color:maroon'>body</span><span
style='font-size:10pt;font-family:"Lucida Console";color:blue'>&gt;</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&lt;/</span><span style='font-size:10pt;font-family:"Lucida Console";
color:maroon'>html</span><span style='font-size:10pt;font-family:"Lucida Console";
color:blue'>&gt;</span><span style='font-size:10pt;font-family:"Lucida Console";
color:black'></span></pre>

<p>The comment tags that did not have content have been deleted. </p>
<p>Let's add some script blocks, one assigned to the “ScriptBlocks:Upper” Injection Point and the other to the “ScriptBlocks:Lower” Injection Point.</p>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console"'><span style='font-size:10pt;font-family:"Lucida Console";
color:black;background:yellow'>@model&nbsp;</span><span style='font-size:10pt;
font-family:"Lucida Console";color:black'>Models.</span><span style='font-size:
10pt;font-family:"Lucida Console";color:#2B91AF'>MyModel</span><span
style='font-size:10pt;font-family:"Lucida Console";color:black'></span><br /><br /><span style="background-color:yellow">@</span>{<br />&nbsp;&nbsp;&nbsp;Injector.ScriptFile(<span style="color:maroon">"~/Scripts/jquery.validate.min.js"</span>);<br />&nbsp;&nbsp;&nbsp;Injector.ScriptFile(<span style="color:maroon">"~/Scripts/jquery.validate.unobtrusive.min.js"</span>);<br />&nbsp;&nbsp;&nbsp;Injector.ScriptBlock(<span style="color:blue">null</span>, <span style="color:maroon">"function test() {alert('hello');}"</span>, 0, <span style="color:maroon">"Upper"</span>);<br />&nbsp;&nbsp;&nbsp;Injector.ScriptBlock(<span style="color:blue">null</span>, <span style="color:maroon">"test();"</span>, 0, <span style="color:maroon">"Lower"</span>);<br />}</pre>





<h3><a name="gettingStarted">Getting started</a></h3>
<ul type='disc'>
<li>
Add the <b>ContentInjector.dll</b> assembly to your ASP.NET MVC web application. Requires MVC 3.0 or higher.
     (Alternatively, add the source code project and set a reference from your
     application to it.)
	  <br />
	  You can add it either using the NuGet Package Manager in Visual Studio or by going here:<br />
	  <a href="https://github.com/plblum/ContentInjector/blob/master/Assemblies/ContentInjector.dll?raw=true">Retrieve assembly</a>
	  </li>
<li>Open the <b>Views\web.config</b> file and locate the &lt;system.web.mvc.razor&gt; section.<br />
<ul type="circle">
<li>Replace the value of <b>pageBaseType</b> with “ContentInjector.CIWebViewPage”.</li>
<li>Add &lt;add namespace="ContentInjector" /&gt; into the &lt;namespaces&gt; block.</li>
</ul>
</li>
<li>In Application_Start, add this code:<br />
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>ViewEngines.Engines.Clear();<br />ViewEngines.Engines.Add(new InsertionsManagement.IMRazorViewEngine());</pre>
</li>
<li>Add the customary Injection Points to the master page, as shown in the <a href="#Example">Example above</a>.</li>
<li>Call methods on the Injector property to add content. Methods include ScriptFile(),
StyleFile(), ScriptBlock(), MetaTag(), HiddenField(),
ArrayDeclaration(), and Placeholder().<br />
<em>Examples:</em><br />
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>Injector.ScriptFile("~/Scripts/jquery-1.9.1.js");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>Injector.StyleFile("~/Content/StyleSheet.css");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>Injector.ScriptBlock("somekey", "some javascript", 0, "Lower");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>Injector.HiddenField("codes", "AB903F");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>Injector.MetaTag("description", "about my site");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>Injector.Placeholder("&lt;!-- xyz library used under license --&gt;");</pre>
<pre style='background:#FFEFE6;font-size:10pt;font-family:"Lucida Console";color:black'>Injector.ArrayDeclaration("myVar", "my string value", 0, "Lower");<br />Injector.ArrayDeclaration("myVar", 100, 0, "Lower");<br />Injector.ArrayDeclarationAsCode("MyVar", "null", 0, "Lower");</pre>
</li>
<li>Open the <a href="https://github.com/plblum/ContentInjector/blob/master/Users%20Guide.pdf?raw=true">
<strong>Users Guide.pdf</strong></a> and dig in. It provides details on all described above.</li>
</ul>
