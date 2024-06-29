import { useAuth } from '../hooks/useAuth'
const Test = () => {
  const { register, login } = useAuth()

  return (
    <>
      <h1 onClick={() => register('test', 'ciapibara@gmail.com', 'chujemujeDzikieW3ze>', 'Ciapibara')}>chujj</h1>
      <h1 onClick={() => login('ciapibara@gmail.com', 'chujemujeDzikieW3ze')}>chuj2</h1>
    </>
  )
}

export default Test
