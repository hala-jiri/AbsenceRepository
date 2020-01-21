# AbsenceRepository

<p>AbsenceWebApp</p>
<p>Web app for requesting and approving absences.</p>

<p>Few rules:</p>
<p>Just <b>employees</b> can making requests for absence</p>
<p>Just <b>management</b> can making approve a requests</p>
<p>Just <b>admin</b> can manage users rights</p>
<br />
<p>Many functionalities not working properly.
Examples:
- admin cannot lock and unlock users (everything is prepared, just view doesnt work properly)
- during overlap absence creating should be warning validation message
- graphic and css style of system is really bad
- datetime picker wasnt implement. (issue with JQuery)
- Unit tests are not working because there is missing mock of user Identity. System is based on approve who have access for data.
</p>
