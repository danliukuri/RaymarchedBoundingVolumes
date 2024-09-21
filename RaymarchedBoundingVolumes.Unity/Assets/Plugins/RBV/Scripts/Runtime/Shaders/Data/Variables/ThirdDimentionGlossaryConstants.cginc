#pragma once

/**
 * The Cartesian product of two shapes A and B, with N and M dimensions respectively,
 * is the shape defined by the set of all points whose
 * first N co-ordinates are the co-ordinates of a point in shape A and
 * last M co-ordinates are the co-ordinates of a point in shape B.
 * The resulting shape has dimension N + M.
 *
 * Examples:\n
 * The Cartesian product of a line and a square is a cube.\n
 * The Cartesian product of two circles is a double cylinder.
 *
 * For more information, visit:\n
 * <a href="http://hi.gher.space/wiki/Cartesian_product">Garrett Jones - Cartesian Product</a>
 *
 * @related cartesianProduct
 */
static int _CartesianProductDefinition;