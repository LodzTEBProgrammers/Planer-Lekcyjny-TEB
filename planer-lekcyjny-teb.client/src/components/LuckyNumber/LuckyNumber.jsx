import React, { useEffect, useState } from "react"
import "./LuckyNumber.css"

const App = () => {
	const [number, setNumber] = useState(null)
	const [inputNumber, setInputNumber] = useState("")
	useEffect(() => {
		const savedNumber = localStorage.getItem("luckyNumber")
		const savedTimestamp = localStorage.getItem("luckyNumberTimestamp")
		const now = new Date().getTime()
		if (savedNumber && savedTimestamp && now - savedTimestamp < 24 * 60 * 60 * 1000) {
			setNumber(parseInt(savedNumber, 10))
		}
	}, [])

	const handleInputChange = (e) => {
		setInputNumber(e.target.value)
	}

	const handleSaveNumber = () => {
		const number = parseInt(inputNumber, 10)
		if (!isNaN(number)) {
			setNumber(number)
			localStorage.setItem("luckyNumber", number)
			localStorage.setItem("luckyNumberTimestamp", new Date().getTime())
		}
	}

	return (
		<div>
			{number === null ? (
				<div className="LN-INSERT mobile-LN">
					<input type="text" value={inputNumber} onChange={handleInputChange} placeholder="Wpisz Szczęśliwy Numerek" />
					<button onClick={handleSaveNumber}>Zapisz</button>
				</div>
			) : (
				<>
					<div className="LN mobile-LN">
						<p>Szczęśliwy Numerek:</p>
						<h1>{number}</h1>
					</div>
				</>
			)}
		</div>
	)
}

export default App
