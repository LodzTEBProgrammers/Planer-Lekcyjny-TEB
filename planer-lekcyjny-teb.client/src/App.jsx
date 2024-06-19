import { RouterProvider, createBrowserRouter, createRoutesFromElements, Route } from "react-router-dom"
import ProtectedRoutes from "@/components/ProtectedRoutes.jsx"
import Home from "@/components/Home.jsx"
import Admin from "@/components/Admin.jsx"
import Login from "./components/Login/Login"
import Register from "./components/Register/Register"
import Timetable from "@/components/Timetable"
import PublicAnnouncements from "@/components/PublicAnnouncements"
import LuckyNumber from "./components/LuckyNumber/LuckyNumber"
import { useState } from "react"

const router = createBrowserRouter(
	createRoutesFromElements(
		<Route path="/">
			<Route element={<ProtectedRoutes />}>
				<Route path="/" element={<Home />} />
				<Route path="/admin" element={<Admin />} />
			</Route>
			<Route path="/login" element={<Login />} />
			<Route path="/register" element={<Register />} />
			<Route path="/plan-lekcji" element={<Timetable />} />
			<Route path="/ogloszenia" element={<PublicAnnouncements />} />
			<Route
				path="*"
				element={
					<div>
						<header>
							<h1>Not Found</h1>
						</header>
						<p>
							<a href="/">Back to Home</a>
						</p>
					</div>
				}
			/>
		</Route>
	)
)
function App() {
	const isLogged = localStorage.getItem("user")

	const logout = async () => {
		const response = await fetch("/api/SecureWebsite/logout", {
			method: "GET",
			credentials: "include",
		})

		const data = await response.json()
		if (response.ok) {
			localStorage.removeItem("user")
			alert(data.message)
			document.location = "/login"
		} else {
			console.log("Could not logout", response)
		}
	}

	const [isclicked, SetIsClicked] = useState(false)

	return (
		<section>
			<div className="top-nav" style={{ display: isclicked ? "flex" : "none" }}>
				{isLogged ? (
					<span className="item-holder">
						<a href="/">Home</a>
						<a href="/admin">Admin</a>
						<a href="/plan-lekcji">Plan Lekcji</a>
						<span onClick={logout}>Log Out</span>
					</span>
				) : (
					<span className="item-holder">
						<a href="/login">Login</a>
						<a href="/register">Register</a>
						<a href="/plan-lekcji">Plan Lekcji</a>
						<a href="/ogloszenia">Ogłoszenia</a>
					</span>
				)}
				<LuckyNumber></LuckyNumber>

				<div
					className="close-mobile"
					onClick={() => {
						SetIsClicked(!isclicked)
					}}
				>
					<span></span>
					<span></span>
				</div>
			</div>
			<div
				className="hamburger"
				onClick={() => {
					SetIsClicked(!isclicked)
				}}
			>
				<span></span>
				<span></span>
				<span></span>
			</div>

			<RouterProvider router={router} />
		</section>
	)
}

export default App
