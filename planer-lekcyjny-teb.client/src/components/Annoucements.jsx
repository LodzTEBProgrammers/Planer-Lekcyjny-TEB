import { useEffect, useState } from "react"
import PropTypes from 'prop-types';

function Announcements({ isAdmin }) {
	const [announcements, setAnnouncements] = useState([]);
	const [newAnnouncement, setNewAnnouncement] = useState('');
	
	
	const handleAddAnnouncement = async () => {
		if (isAdmin) {
			fetch('https://localhost:7273/api/SecureWebsite/admin/announcement', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: JSON.stringify({ content: newAnnouncement }),
			})
				.then(response => {
					console.log('Response status:', response.status, response.statusText); // log the response status and status text

					if (!response.ok) {
						throw new Error('Network response was not ok');
					}
					return response.json();
				})
				.then(data => {
					setAnnouncements([...announcements, data]);
					setNewAnnouncement('');
				})
				.catch((error) => {
					console.error('Error:', error);
				});
		}
	};

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
		<section className='admin-page page'>
			<header>
				<h1>Admin Panel - Ogłoszenia</h1>
			</header>
				<section>
					{isAdmin && (
						<div>
							<h3>Dodaj nowe ogłoszenie:</h3>
							<input
								type="text"
								value={newAnnouncement}
								onChange={(e) => setNewAnnouncement(e.target.value)}
							/>
							<button onClick={handleAddAnnouncement}>Add Announcement</button>
						</div>
					)}
					<div>
						<h2>Ogłoszenia:</h2>
						<ul>
							{announcements.map((announcement) => {
								const date = new Date(announcement.date);
								const day = date.toLocaleDateString();

								return (
									<li key={announcement.id}>
										<div style={{ border: '1px solid black', padding: '10px', margin: '10px' }}>
											{announcement.content } - <span style={{ color: 'red', fontWeight: 'bold' }}>{day} </span>
											{ date.toLocaleTimeString()}
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


Announcements.propTypes = {
	isAdmin: PropTypes.bool.isRequired,
};
export default Announcements;