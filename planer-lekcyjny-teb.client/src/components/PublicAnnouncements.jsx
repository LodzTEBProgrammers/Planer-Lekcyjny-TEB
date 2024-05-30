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

export default PublicAnnouncements;