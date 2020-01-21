# AbsenceRepository

<h4>AbsenceWebApp</h4>
<p>Web app for requesting and approving absences.</p>

<p>Few rules:</p>
<p>Just <b>employees</b> can making requests for absence</p>
<p>Just <b>management</b> can making approve a requests</p>
<p>Just <b>admin</b> can manage users rights</p>
<br />
<p>Many functionalities not working properly.
Examples:
<ul>
	<li>admin cannot lock and unlock users (everything is prepared, just view doesnt work properly)</li>
	<li>during overlap absence creating should be warning validation message</li>
	<li>graphic and css style of system is really bad</li>
	<li>datetime picker wasnt implement. (issue with JQuery)</li>
	<li>Unit tests are not working because there is missing mock of user Identity. System is based on approve who have access for data.</li>
</ul>
</p>
<br />
<h4>What was used in project</h4>
<ul>
	<li>Dependency Injection</li>
	<li>MVC architecture</li>
	<li>Linq</li>
	<li>non generic repository (generic one is prepared)</li>
	<li>inheritance</li>
	<li>interface</li>
	<li>partial view</li>
	<li>...</li>
</ul>

