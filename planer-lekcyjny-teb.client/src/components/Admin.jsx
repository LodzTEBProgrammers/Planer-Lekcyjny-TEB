import Announcements from "@/components/Annoucements"
import AdminLuckyNumber from "./LuckyNumber/AdminLuckyNumber"

function Admin() {
	document.title = "Admin Panel"

	return (
		<section className="admin-page page">
			<header>
				<Announcements isAdmin={true} />
			</header>
			<AdminLuckyNumber></AdminLuckyNumber>
		</section>
	)
}

export default Admin
