import React from "react"

const handleRemoveNumber = () => {
	localStorage.removeItem("luckyNumber")
	localStorage.removeItem("luckyNumberTimestamp")
	window.location.reload()
}
const AdminLuckyNumber = () => {
	return (
		<div>
			<button onClick={handleRemoveNumber} className="Usun">
				Usuń Szczęśliwy Numerek
			</button>
		</div>
	)
}

export default AdminLuckyNumber
