import { useEffect, useState } from "react"

function Announcements() {
	const [announcements, setAnnouncements] = useState([]);
	const [newAnnouncement, setNewAnnouncement] = useState('');

	const handleAddAnnouncement = async () => {
		fetch('https://localhost:7273/api/SecureWebsite/admin/announcement', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
				"Accept": "application/json",
			},
			body: JSON.stringify({ content: newAnnouncement }),
		})
			.then(response => {
				console.log('Response status:', response.status, response.statusText); // log the response status and status text
				
				return response.json();
			})
			.then(data => {
				setAnnouncements([...announcements, data]);
				setNewAnnouncement('');
			})
			.catch((error) => {
				console.error('Error:', error);
			});
	};

	useEffect(() => {
		fetch('https://localhost:7273/api/SecureWebsite/admin/announcement')
			.then(response => {
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
		<section className='admin-page page'>
			<header>
				<h1 style={{textDecoration: 'underline'}}>Admin Panel - Ogłoszenia</h1>
			</header>
				<section>
						<div>
							<h3>Dodaj nowe ogłoszenie:</h3>
							<input
								type="text"
								value={newAnnouncement}
								onChange={(e) => setNewAnnouncement(e.target.value)}
							/>
							<button onClick={handleAddAnnouncement}>Dodaj ogłoszenie</button>
						</div>
					<div>
						<h2>Ogłoszenia:</h2>
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


export default Announcements;