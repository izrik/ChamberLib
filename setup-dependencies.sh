#!/bin/bash

git submodule init
git submodule update

git submodule foreach --recursive 'git submodule init && git submodule update'
git submodule foreach --recursive 'if [ -e ./setup-submodules.sh -a -x ./setup-submodules.sh ]; then ./setup-submodules.sh ; fi'
git submodule foreach --recursive 'if [ -e ./setup-dependencies.sh -a -x ./setup-dependencies.sh ]; then ./setup-dependencies.sh ; fi'

PWD_BAK=`pwd`
pushd assimp && echo dont git checkout develop && cmake -DCMAKE_OSX_ARCHITECTURES=i386 -G "Unix Makefiles" . && make && cp lib/libassimp.dylib ../assimp-net/libs/Assimp/ && popd
cd $PWD_BAK
