#pragma once

#define DEFINE_STACK(Type, name, size) \
    static Type name[size]; \
    static int name##Top = -1; \
    \
    bool is##name##Empty() \
    { \
        return name##Top < 0; \
    } \
    \
    bool is##name##Full() \
    { \
        return name##Top >= size - 1; \
    } \
    \
    void pushTo##name(Type value) \
    { \
        name[++name##Top] = value; \
    } \
    \
    Type popFrom##name() \
    { \
        return name[name##Top--]; \
    }