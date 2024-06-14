import { useEffect, useState } from 'react';
import './Timetable.css';

function Timetable() {
	const [timetable, setTimetable] = useState([]);
	const [selectedLesson, setSelectedLesson] = useState(null);

	useEffect(() => {
		fetch('https://localhost:7273/Card')
			.then(response => {
				if (!response.ok) {
					throw new Error(`HTTP error! status: ${response.status}`);
				}
				return response.json();
			})
			.then(data => {
				setTimetable(data);
				setSelectedLesson(data[0]); // Select the first lesson by default
			})
			.catch(error => {
				console.error('Error:', error);
			});
	}, []);

	let lessonStartTime = '';
	let lessonEndTime = '';
	let lessonDay = '';
	if (selectedLesson) {
		lessonStartTime = selectedLesson.startTime.substring(0, 5);
		lessonEndTime = selectedLesson.endTime.substring(0, 5);
		lessonDay = selectedLesson.day;
	}
	
	return (
		<div className="timetable-container">
			<h1 className="timetable-title">Plan Lekcji</h1>
			{selectedLesson && (
				<h3 className="timetable-subtitle">
					Lekcje na godzinę [<span className="time">{lessonStartTime} - 
					{lessonEndTime}</span>] w dniu [<span className='day'>{lessonDay}</span>]
				</h3>
			)}
			<table className="timetable-table">
				<thead>
				<tr>
					<th>Lekcja</th>
					<th>Klasa</th>
					<th>Nauczyciel</th>
					<th>Sala</th>
				</tr>
				</thead>
				 <tbody>
                {Array.isArray(timetable) ? timetable.map((lesson, index) => (
                    <tr key={index} className="timetable-row">
                        <td>{lesson.lesson}</td>
                        <td>{lesson.class}</td>
                        <td>{lesson.teacher}</td>
                        <td>{lesson.classroom}</td>
                    </tr>
                )) : <tr><td colSpan="4"><span className='end-lessons-message'>{timetable?.message}</span></td></tr>}
                </tbody>
			</table>
		</div>
	);
}

export default Timetable;