import { useEffect, useState } from "react"

function PublicAnnouncements() {
	const [announcements, setAnnouncements] = useState([]);

	useEffect(() => {
		fetch('https://localhost:7273/api/SecureWebsite/admin/announcement')
			.then(response => {
				if (!response.ok) {
					throw new Error('Network response was not ok');
				}
				return response.json();
			})
			.then(data => {
				setAnnouncements(data);
			})
			.catch((error) => {
				console.error('Error:', error);
			});
	}, []);

	return (
		<section className='public-page page'>
			<section>
				<div>
					<h1>Ogłoszenia:</h1>
					<ul>
						{announcements
							.sort((a, b) => new Date(b.date) - new Date(a.date)).map((announcement) => {
							const announcementDate = new Date(announcement.date);
							const today = new Date();

							// Check if the announcement date is the same as today's date
							const isToday = announcementDate.getDate() === today.getDate() &&
								announcementDate.getMonth() === today.getMonth() &&
								announcementDate.getFullYear() === today.getFullYear();

							const day = announcementDate.toLocaleDateString();

							return (
								<li key={announcement.id}>
									<div style={{ border: '1px solid black', padding: '10px', margin: '10px' }}>
										{announcement.content } -
										<span style={{ color: isToday ? 'red' : 'black', fontWeight: 'bold' }}> {day} </span>
										{ announcementDate.toLocaleTimeString()}
									</div>
								</li>
							);
						})}
					</ul>
				</div>
			</section>
		</section>
	);
}


export default PublicAnnouncements;