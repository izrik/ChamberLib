#!/bin/sh

#
# ChamberLib, a cross-platform game engine
# Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
#
# This library is free software; you can redistribute it and/or
# modify it under the terms of the GNU Lesser General Public
# License as published by the Free Software Foundation; either
# version 2.1 of the License, or (at your option) any later version.
#
# This library is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
# Lesser General Public License for more details.
#
# You should have received a copy of the GNU Lesser General Public
# License along with this library; if not, write to the Free Software
# Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
# USA
#

echo ""
echo "Long lines:"

#grep -n '................................................................................' $(find . -name \*.cs)
git diff -U0 | \
    grep -v -e '^@@' -e '^diff --git' -e '^+++' -e '^---' -e '^-' | \
    grep -n '[-+]................................................................................'
#                12345678901234567890123456789012345678901234567890123456789012345678901234567890
git diff --cached -U0 | \
    grep -v -e '^@@' -e '^diff --git' -e '^+++' -e '^---' -e '^-' | \
    grep -n '[-+]................................................................................'
#                12345678901234567890123456789012345678901234567890123456789012345678901234567890

echo ""
echo "Trailing whitespace:"

git diff -U0 | \
    grep -v -e '^@@' -e '^diff --git' -e '^+++' -e '^---' -e '^-' | \
    grep -n '[ 	]$'  # space or literal tab
git diff --cached -U0 | \
    grep -v -e '^@@' -e '^diff --git' -e '^+++' -e '^---' -e '^-' | \
    grep -n '[ 	]$'  # space or literal tab
#grep -rnE '[ 	]$' \
# $(git ls-files)

# TODO: check for tabs rather than spaces
# TODO: check for trailing whitespace on lines
# TODO: check for single newline at end of file
# TODO: count TODO's

echo ""
echo "Files missing license notices:"
grep -L 'GNU Lesser General Public' \
    $(find . -name \*.cs -o -name \*.sh |
        grep -v -e '^./obj/' -e '^./ChamberLibTests/obj/' )
