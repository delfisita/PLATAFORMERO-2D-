# PLATAFORMERO 2D

Este proyecto contiene un ejemplo de script para controlar a un jugador en un juego de plataformas 2D en Unity.

## Características
- Movimiento horizontal
- Salto
- Dash
- Agacharse

### Uso
1. Crea un objeto `Player` en tu escena con un `Rigidbody2D` y dos `Collider2D` (uno para estar de pie y otro para agachado).
2. Añade el script `PlayerMovement` al objeto.
3. Asigna las referencias de los `Collider2D` en el inspector.
4. Configura las teclas:
   - Movimiento: `Horizontal` (A/D o flechas)
   - Salto: `Jump` (espacio por defecto)
   - Dash: `Left Shift`
   - Agacharse: Flecha abajo

## Próximos pasos
- Añadir mecánica de luz como power-up.
