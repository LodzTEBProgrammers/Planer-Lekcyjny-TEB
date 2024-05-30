import Announcements from "@/components/Annoucements"

function Admin() {

	document.title = "Admin Panel";
	
	return (
		<section className='admin-page page'>
			<header>
				<Announcements isAdmin={true} /> 
			</header>
		</section>
	);
}

export default Admin;